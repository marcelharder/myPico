
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Appointment } from 'src/app/_models/appointment';
import { requestDays } from 'src/app/_models/requestDays';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { DaysService } from 'src/app/_services/days.service';
import { GeneralService } from 'src/app/_services/general.service';
import { MonthSummaryComponent } from './first-month-summary/first-month-summary.component';
import { FirstMonthComponent } from './first-month/first-month.component';
import { SecondMonthSummaryComponent } from './second-month-summary/second-month-summary.component';
import { SecondMonthComponent } from './second-month/second-month.component';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  currentPicoUnitId = 0;
  selectedCurrency = "PHP";
  desiredCurrency = "PHP";
  request = 0;
  appt: Appointment = {
    picoUnitId: 0,
    picoUnitPhotoUrl: "",
    userId: 0,
    requestedDays: [],
    startDate: new Date,
    endDate: new Date,
    noOfNights: 0,
    id: 0,
    year: 0,
    month: 0,
    day: 0,
    status: "",
    rent: 0,
    rentUSD: 0,
    downPayment: 0,
    paid_InFull: 0,
    comment: ""};
  


  currentYear = 0;
  currentMonth = 0;
  location = "";
  firstMonth: RequestedMonth = { appointmentId: 0, picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { appointmentId: 0, picoUnit: 0, year: 0, month: 0 };
  appointmentId = 0;
  requestDay: requestDays = { daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 };
  firstMonthRequest: Array<string> = [];
  secondMonthRequest: Array<string> = [];
  firstMonthRequestedDays: Array<requestDays> = [];
  secondMonthRequestedDays: Array<requestDays> = [];


  @ViewChild(FirstMonthComponent) fm!: FirstMonthComponent;
  @ViewChild(SecondMonthComponent) sm!: SecondMonthComponent;

  

  constructor(private auth: AuthService,
    public gen: GeneralService,
    private days: DaysService,
    private route: ActivatedRoute,
    private alertify: AlertifyService) {

  }

  ngOnInit() {

   this.auth.firstMonth.subscribe((next) => { 
      this.firstMonth = next; 
     
    })
    this.auth.secondMonth.subscribe((next) => { 
      this.secondMonth = next;
    
    })

    this.auth.currentPicoUnit.subscribe((next)=>{
       this.currentPicoUnitId = +next;
      this.gen.getPicoUnitName(this.currentPicoUnitId).subscribe((next)=>{
        this.location = next;
      })
    })
  }

  showPhp() { this.alertify.message("PHP"); }
  showUsd() { this.alertify.message("USD"); }

  sendMessage(m: any){
    
  }

  currencyChanged() {
    for (let i = 0; i < this.firstMonthRequestedDays.length; i++) {
      let item: requestDays = this.firstMonthRequestedDays[i];
      // get the price from the backend
      this.gen.getUnitPrice(this.currentPicoUnitId, this.desiredCurrency, item.daynumber, +item.month).subscribe((next)=>{
        item.price = next;
      })
    }

    this.alertify.success(this.desiredCurrency);
  }

  prevMonth() {
    this.alertify.confirm("This will erase your current request ...", () => {
      this.fm.makeVacant();
      this.sm.makeVacant();
      this.firstMonthRequestedDays = [];
      this.secondMonthRequestedDays = [];
          

      // allow for jumping from january to december

      if (this.firstMonth.month === 1) {
        this.firstMonth.month = 12;
        this.firstMonth.year = this.firstMonth.year - 1;
        this.fm.nextMonth(this.firstMonth);
        this.secondMonth.month = this.secondMonth.month - 1;
        this.sm.nextMonth(this.secondMonth);
      } else {
        if (this.secondMonth.month === 1) {
          this.secondMonth.month = 12;
          this.secondMonth.year = this.secondMonth.year - 1;
          this.sm.nextMonth(this.secondMonth);
          this.firstMonth.month = this.firstMonth.month - 1;
          this.fm.nextMonth(this.firstMonth);
        } else {
          this.firstMonth.month = this.firstMonth.month - 1;
          this.secondMonth.month = this.secondMonth.month - 1;
          this.fm.nextMonth(this.firstMonth);
          this.sm.nextMonth(this.secondMonth);
        }
      }






    })
  }
  nextMonth() {
    this.alertify.confirm("This will erase your current request ...", () => {
      this.fm.makeVacant();
      this.sm.makeVacant();
      this.firstMonthRequestedDays = [];
      this.secondMonthRequestedDays = [];


      // allow for jumping from december to january
      if (this.secondMonth.month === 12) {
        this.secondMonth.month = 1;
        this.secondMonth.year = this.secondMonth.year + 1;
        this.sm.nextMonth(this.secondMonth);
        this.firstMonth.month = this.firstMonth.month + 1;
        this.fm.nextMonth(this.firstMonth);
      } else {
        if (this.firstMonth.month === 12) {
          this.firstMonth.month = 1;
          this.firstMonth.year = this.firstMonth.year + 1;
          this.fm.nextMonth(this.firstMonth);
          this.secondMonth.month = this.secondMonth.month + 1;
          this.sm.nextMonth(this.secondMonth);
        } else {
          this.firstMonth.month = this.firstMonth.month + 1;
          this.secondMonth.month = this.secondMonth.month + 1;
          this.fm.nextMonth(this.firstMonth);
          this.sm.nextMonth(this.secondMonth);
        }
      }



    })
  }
  receiveUpdatesFirstMonth(dates: Array<string>) {
    this.firstMonthRequestedDays = [];

    for (let i = 0; i < dates.length; i++) { // this is a list of string, like 17,18,19 etc
      let help: requestDays = { daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 };
      
      this.gen.getUnitPrice(this.currentPicoUnitId, this.selectedCurrency, +dates[i], this.firstMonth.year)
        .subscribe(
        (next) => { help.price = next }, 
        (error) => { this.alertify.error(error) }, 
        () => {// do the rest when this observable is finished
          help.daynumber = +dates[i];
          help.month = this.firstMonth.month.toString();
          help.year = this.firstMonth.year;
          help.date.setUTCFullYear(help.year);
          help.date.setUTCMonth(+help.month);
          help.date.setDate(help.daynumber);
          this.firstMonthRequestedDays.push(help);
        });
    }
  }

  receiveUpdatesSecondMonth(dates: Array<string>) {
    this.secondMonthRequestedDays = [];
    for (let i = 0; i < dates.length; i++) {
      let help: requestDays = { daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 }
      this.gen.getUnitPrice(this.currentPicoUnitId, this.selectedCurrency,  +dates[i], this.firstMonth.year)
        .subscribe((next) => { help.price = next }, (error) => { this.alertify.error(error) }, () => {
          // do the rest when this observable is finished
          help.daynumber = +dates[i];
          help.month = this.secondMonth.month.toString();
          help.year = this.secondMonth.year;
          help.date.setUTCFullYear(help.year);
          help.date.setUTCMonth(+help.month);
          help.date.setDate(help.daynumber);
          this.secondMonthRequestedDays.push(help);
        });
    }


  }



  getMonthText(test: number) { return this.gen.getMonthFromNo(test); }

  showRequest(){
    
    if(this.request === 1){return true;} else {return false;}}


}




