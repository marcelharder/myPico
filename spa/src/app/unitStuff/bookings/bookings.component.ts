import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { DaysService } from 'src/app/_services/days.service';
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
  firstMonth: RequestedMonth = { Id: 0, picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { Id: 0, picoUnit: 0, year: 0, month: 0 };
  currentYear = 0;
  currentMonth = 0;
  currentMonthId = 0;
  counter = 0;

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
    private days: DaysService,
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
    // get the current month number from the backend
    this.days.getMonthId(this.currentMonth, this.currentYear).subscribe((next) => {
      this.currentMonthId = next;
      // push it to the calendar # 1, Nb the month from typescript is zerobased and my stuff not :-)
      this.firstMonth.Id = this.currentMonthId + 1;
      this.firstMonth.picoUnit = this.currentPicoUnitId;
      this.fm.nextMonth(this.firstMonth);
      // push it to the calendar # 2
      this.secondMonth.Id = this.currentMonthId + 2;
      this.secondMonth.picoUnit = this.currentPicoUnitId;
      this.sm.nextMonth(this.secondMonth);

    })
  }

  prevMonth() {
    this.alertify.error("getting previous month");
    this.firstMonth.Id = this.firstMonth.Id - 1;
    this.secondMonth.Id = this.secondMonth.Id - 1;


    this.fm.nextMonth(this.firstMonth);
    this.sm.nextMonth(this.secondMonth);
  }
  nextMonth() {
    this.alertify.error("getting next month");
    this.firstMonth.Id = this.firstMonth.Id + 1;
    this.secondMonth.Id = this.secondMonth.Id + 1;

    this.fm.nextMonth(this.firstMonth);
    this.sm.nextMonth(this.secondMonth);
  }



}
