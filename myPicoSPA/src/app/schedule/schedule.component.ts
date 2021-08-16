import { SmsService } from './../_services/sms.service';
import { Component, OnInit, ViewChild, ElementRef, Output, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


import { OccupancyService } from '../_services/occupancy.service';
import { Utilities } from '../_helpers/utilities';
import { Appointment } from '../_models/appointment';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { RequestedMonth } from '../_models/RequestedMonth';
import { FirstMonthComponent } from './first-month/first-month.component';
import { SecondMonthComponent } from './second-month/second-month.component';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { AppointmentService } from '../_services/appointment.service';
import { User } from '../_models/user';
import { getDayOfYear, setDayOfYear } from 'ngx-bootstrap/chronos/units/day-of-year';
import { DaysService } from '../_services/days.service';
import { DaysModel } from '../_models/daysModel';
@Component({
    templateUrl: './schedule.component.html',
    styleUrls: ['./schedule.component.css']
})


export class ScheduleComponent implements OnInit {
    @ViewChild(FirstMonthComponent) fm: FirstMonthComponent;
    @ViewChild(SecondMonthComponent) sm: SecondMonthComponent;
    firstMonth: RequestedMonth = new RequestedMonth(1, 0, 2018);
    secondMonth: RequestedMonth = new RequestedMonth(1, 0, 2018);
    listDaysArray: Array<DaysModel> = [];
    bsConfig: Partial<BsDatepickerConfig>;

    currentMonth = 0;
    currentYear = 0;
    selectedUnit: string;
    mb: Appointment;
    util: Utilities = new Utilities();
    hmos: Array<string> = this.util.getMonths();
    years: Array<string> = this.util.getYears();
    listDays: Array<string>;
    lo: Array<string>;
    listOccupancy: Array<string>;
    currentUser: User;
    picoUnitId: number;
    id: number;
    arrivalDate: Date;
    dischargeDate: Date;
    requestedDays: string[] = [];
    requestedDaysPrices: string[] = [];
    requestedDaysSeason: string[] = [];
    totalRent: number;
    selectedMonth: string;
    selectedYear: string;
    allowAddingOccupancy = false;
    allowDeletingOccupancy = false;

    constructor(private auth: AuthService,
        private dayService: DaysService,
        private alertify: AlertifyService,
        private appts: AppointmentService,
        private sms: SmsService,
        private r: Router,
        private route: ActivatedRoute) { }

    ngOnInit() {

        this.picoUnitId = 1;
        this.selectedUnit = "610-A";
        this.bsConfig = { containerClass: 'theme-blue' };
        // find the current date
        const d = new Date();
        this.currentMonth = d.getMonth();
        this.currentYear = d.getFullYear();
        // set the dropdownlists to the current date,

        this.selectedMonth = this.util.translateMonth(this.currentMonth);
        this.selectedYear = this.currentYear.toString();
        // set to the current date
        this.arrivalDate = new Date();
        this.dischargeDate = new Date();
        this.dischargeDate.setDate(this.dischargeDate.getDate() + 1);

    }

    showItInTheCalendars() {
        // show it in the calendars
        this.firstMonth.year = this.currentYear;
        this.firstMonth.month = this.currentMonth;
        this.fm.callFromAbove();

        if (this.currentMonth === 12) {
            this.secondMonth.year = this.currentYear + 1;
            this.secondMonth.month = 0;
        } else {
            this.secondMonth.year = this.currentYear;
            this.secondMonth.month = this.currentMonth + 1;
        }
        this.sm.callFromAbove();
    }

    buttonclicked() { // the date dropdownlists are changed
        this.arrivalDate = new Date(+this.selectedYear, this.util.getNumberOfMonth(this.selectedMonth), 1);
        this.dischargeDate = new Date(+this.selectedYear, this.util.getNumberOfMonth(this.selectedMonth), 2);
        this.currentMonth = this.util.getNumberOfMonth(this.selectedMonth);
        this.currentYear = +this.selectedYear;
        this.showItInTheCalendars();
        this.constructTheCalendarModel();
    }
    arrivalDateChanged(event) {
        const help = new Date(event);
        // update the dropdownlists
        this.selectedMonth = this.util.translateMonth(help.getMonth());
        this.selectedYear = String(help.getUTCFullYear());
        // go to the calendars
        this.currentMonth = help.getMonth();
        this.currentYear = +this.selectedYear;
        this.showItInTheCalendars();
    }
    dischargeDateChanged(event) {
        const help = new Date(event);
        // adjust the discharge date
        this.dischargeDate = help;
    }
    requestBooking() {
        if (this.auth.loggedIn()) {
            if (this.requestedDays.length === 0) {
                this.alertify.error('Select some dates first');
            } else {
                // make new appointment
                this.mb = new Appointment();
                this.mb.picoUnitId = this.picoUnitId;
                this.mb.userId = this.auth.currentUser.id;
                this.mb.startDate = this.arrivalDate;
                this.mb.endDate = this.dischargeDate;
                this.mb.requestedDays = this.requestedDays;
                this.mb.year = this.firstMonth.year;
                this.mb.noOfNights = 1; // the number of nights is calculated on the server
                // tijdelijke oplossing
                if (this.picoUnitId === 1) { this.mb.picoUnitPhotoUrl = "../../assets/images/unit-pictures/610-A/nummer-610-A.jpg"; }
                if (this.picoUnitId === 2) { this.mb.picoUnitPhotoUrl = "../../assets/images/unit-pictures/620-A/nummer-620-A.jpg"; }
                if (this.picoUnitId === 3) { this.mb.picoUnitPhotoUrl = "../../assets/images/unit-pictures/640-A/nummer-640-A.jpg"; }
                this.appts.saveAppointment(this.mb.userId, this.mb).subscribe((appt) => {
                    this.sms.sendBookingAlertBySMS(appt.id);
                    this.r.navigate(['/booking']);
                },
                    (er) => { this.alertify.error("This booking was NOT saved"); });
            }
        } else {
            // make sure that the user comes back here after login
            this.cancelBooking();
            this.auth.setReturnUrl('/schedule');
            this.alertify.error("Please login first ...");
        }
    }
    cancelBooking() {
        // set to the current date
        this.arrivalDate = new Date();
        this.dischargeDate = new Date();
        this.dischargeDate.setDate(this.dischargeDate.getDate() + 1);

        // maak alle items met requested CSS weer vacant, send opdracht naar first month
        this.fm.makeVacant();
        this.fm.obs = false;
        this.fm.bordersequence = false;
        // maak alle items met requested CSS weer vacant, send opdracht naar second month
        this.sm.makeVacant();
        this.sm.obs = false;
        this.sm.bordersequence = false;


        this.constructTheCalendarModel();
        this.requestedDays = [];
        this.requestedDaysPrices = [];
        this.requestedDaysSeason = [];
        this.totalRent = 0;
    }
    addOccupancy($event) {
        // change the arrival dates
        if (this.requestedDays.length === 0) { this.arrivalDate = new Date($event); } // get arrival date only once
        this.dischargeDate = new Date($event);
        this.dischargeDate = this.util.addDays(this.dischargeDate, 1);

        if (this.checkOBS(new Date($event))) { // You can only add a reservation if this results in a continuous sequence of dates, therefore 'one beyond sequence is checked'
            this.allowAddingOccupancy = true;
            this.fm.obs = true;
            this.sm.obs = true;
            this.requestedDays.push($event);
            //  do not allow duplicate entries
            this.requestedDays = this.requestedDays.reduce(function (a, b) { if (a.indexOf(b) < 0) { a.push(b); } return a; }, []);
            // const help = this.requestedDays[this.requestedDays.length - 1];
            // get price of this day
            const dayPrice = "5100";
            this.requestedDaysPrices.push(dayPrice);
            const daySeason = "low";
            this.requestedDaysSeason.push(daySeason);
            this.totalRent = this.calculateTotalRent();
        } else {
         this.allowAddingOccupancy = false;
         this.fm.obs = false;
         this.sm.obs = false;
         this.alertify.error("Dates should be a continuous sequence");
        }

    }
    removeOccupancy($event) {
        // you can only remove the last item in the list
        if (this.checkBOS(new Date($event))) {
            this.fm.bordersequence = true;
            this.sm.bordersequence = true;

            this.requestedDaysPrices.splice(this.requestedDays.indexOf($event), 1);
            this.requestedDaysSeason.splice(this.requestedDays.indexOf($event), 1);
            this.requestedDays.splice(this.requestedDays.indexOf($event), 1);
            this.totalRent = this.calculateTotalRent();

        } else { this.fm.bordersequence = true; this.sm.bordersequence = true; }

    }
    calculateTotalRent() {
        let help = 0;
        this.requestedDaysPrices.forEach((item, index) => {
            help = help + +item;
        });
        return help;
    }
    constructTheCalendarModel() {
        this.listDaysArray = []; // clear any previous items, the listDaysArray is the model of the two calendar elements, so we can deal with appointmnts spanning the months
        this.listDaysArray = this.util.getListDaysArray(this.currentYear, this.currentMonth);
    }
    checkOBS(test: Date): boolean {
        // find the index of this date in the listDaysArray, with underscore library
        // const help: DaysModel = _.where(this.listDaysArray, { date: test });
        const hes = new Utilities();
        const day_2 = hes.daysIntoYear(test);
        const day_1 = 0;
        function zoekKersen(fruit) { if (hes.daysIntoYear(fruit.date) === day_2) { return fruit; } return null; }
        function zoekEenValueVanEen(fruit) { if (fruit.value === 1) { return fruit; } return null; }
        // check of dit de eerste requested day is
        if (this.listDaysArray.find(zoekEenValueVanEen) == null) {
           
            const help: DaysModel = this.listDaysArray.find(zoekKersen);
            help.value = 1; return true;
        } else {
            const help: DaysModel = this.listDaysArray.find(zoekKersen);
            const previous_item: DaysModel = this.listDaysArray[help.index - 1];
            const next_item: DaysModel = this.listDaysArray[help.index + 1];
           
            if (help.index !== this.listDaysArray.length) {
                if (previous_item.value === 1 || next_item.value === 1) {
                    help.value = 1; return true; } else {
                        return false; }
            } else {
                if (previous_item.value === 1) { help.value = 1; return true; }
            }
        }
    }




    checkBOS(test: Date): boolean {
        // see if the test date is directly adjacent to the existing dates
        // make sure the test date is not in the existing array
        const hes = new Utilities();
        const day_2 = hes.daysIntoYear(test);
        const day_1 = 0;
        function zoekKersen(fruit) { if (hes.daysIntoYear(fruit.date) === day_2) { return fruit; } return null; }

        const help: DaysModel = this.listDaysArray.find(zoekKersen);
        const previous_item: DaysModel = this.listDaysArray[help.index - 1];
        const next_item: DaysModel = this.listDaysArray[help.index + 1];


        if (help.index === 0) { if (next_item.value === 1) { help.value = 0; return true; } return false; }
        if (help.index === this.listDaysArray.length) { if (previous_item.value === 1) { help.value = 0; return true; } return false; }
        if (previous_item.value === 1 && next_item.value === 1) { return false; }
        if (previous_item.value === 0 && next_item.value === 0) { help.value = 0; return true; }
        help.value = 0;
        return true;
    }



}

