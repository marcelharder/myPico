import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';
import { FirstMonthComponent } from './first-month/first-month.component';
import { SecondMonthComponent } from './second-month/second-month.component';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  currentPicoUnitId = 0;
  location = "";
  firstMonth: RequestedMonth = { picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { picoUnit: 0, year: 0, month: 0 };
  currentYear = 0;
  currentMonth = 0;
  selectedMonth = 0;
  selectedYear = 0;

  @ViewChild(FirstMonthComponent) fm!: FirstMonthComponent;
  @ViewChild(SecondMonthComponent) sm!: SecondMonthComponent;

  //secondMonth!: RequestedMonth;
  /* listDaysArray: Array<DaysModel> = [];
  bsConfig!: Partial<BsDatepickerConfig>;

  currentMonth = 0;
  currentYear = 0;
  selectedUnit: string = "";
  mb!: Appointment;
  util: Utilities = new Utilities();
  hmos: Array<string> = this.util.getMonths();
  years: Array<string> = this.util.getYears();
  listDays: Array<string> =[];
  lo: Array<string>=[];
  listOccupancy: Array<string>=[];
  currentUser!: User;
  picoUnitId: string = "";
  id: number =0;
  arrivalDate!: Date;
  dischargeDate!: Date;
  requestedDays: string[] = [];
  requestedDaysPrices: string[] = [];
  requestedDaysSeason: string[] = [];
  totalRent: number = 0;
  selectedMonth!: string;
  selectedYear!: string;
  allowAddingOccupancy = false;
  allowDeletingOccupancy = false; */

  constructor(private auth: AuthService,
    private gen: GeneralService,
    private route: ActivatedRoute,
    private alertify: AlertifyService) { }

  ngOnInit() {

    this.currentPicoUnitId = +this.route.snapshot.params.id;

    if (this.currentPicoUnitId === 1) { this.location = "Myna 610-A" }
    if (this.currentPicoUnitId === 2) { this.location = "Myna 611-A" }
    if (this.currentPicoUnitId === 3) { this.location = "Myna 612-A" }

    let dateTime = new Date();
    // zet het jaar op het huidige jaar
    this.currentYear = dateTime.getFullYear();
    // zet de maand op de huidige maand
    this.currentMonth = dateTime.getMonth();

    this.firstMonth.year = this.currentYear;
    this.firstMonth.picoUnit = this.currentPicoUnitId;
    this.firstMonth.month = this.currentMonth;

    this.secondMonth.year = this.currentYear;
    this.secondMonth.picoUnit = this.currentPicoUnitId;
    this.secondMonth.month = this.currentMonth + 1;

  }

  removeOccupancy(t: any) { }

  addOccupancy(t: any) { }

  prevMonth() {
    this.alertify.error("getting next month");
    let year = 0;
    let month = 0;
    let help = this.currentMonth;


    month = help - 1;
    this.currentMonth = this.currentMonth -1;
     

    this.sm.nextMonth(this.currentPicoUnitId, year, help);
    this.fm.nextMonth(this.currentPicoUnitId, year, month);
  }
  nextMonth() {
    this.alertify.error("getting next month");
    let year = 0;
    let month = 0;
    let help = this.currentMonth;

    month = help + 1;
    this.currentMonth = this.currentMonth + 1;

    this.sm.nextMonth(this.currentPicoUnitId, year, help);
    this.fm.nextMonth(this.currentPicoUnitId, year, month);
  }



}
