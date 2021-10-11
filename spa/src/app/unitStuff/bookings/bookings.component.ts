
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { requestDays } from 'src/app/_models/requestDays';
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
  selectedCurrency = "PHP";
  desiredCurrency = "";
 
  
  currentYear = 0;
  currentMonth = 0;
  location = "";
  firstMonth: RequestedMonth = { Id: 0, picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { Id: 0, picoUnit: 0, year: 0, month: 0 };
  currentMonthId = 0;
  requestDay: requestDays = { daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 };
  firstMonthRequest: Array<string> = [];
  secondMonthRequest: Array<string> = [];
  firstMonthRequestedDays: Array<requestDays> = [];
  secondMonthRequestedDays: Array<requestDays> = [];


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
    public gen: GeneralService,
    private days: DaysService,
    private route: ActivatedRoute,
    private alertify: AlertifyService) { 
  
    }

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

  showPhp(){this.alertify.message("PHP");}
  showUsd(){this.alertify.message("USD");}

  currencyChanged(){
    for(let i = 0; i < this.firstMonthRequestedDays.length; i++){
       let item:requestDays = this.firstMonthRequestedDays[i];
       item.price = convertCurrency(item.price, this.selectedCurrency, this.desiredCurrency);
    }
    
    this.alertify.success(this.desiredCurrency);}

  prevMonth() {
    this.alertify.confirm("This will erase your current request ...",()=>{
      this.fm.makeVacant();
      this.sm.makeVacant();
      this.firstMonthRequestedDays = [];
      this.secondMonthRequestedDays = [];
      this.firstMonth.Id = this.firstMonth.Id - 1;
      this.secondMonth.Id = this.secondMonth.Id - 1;
  
      this.fm.nextMonth(this.firstMonth);
      this.sm.nextMonth(this.secondMonth);
    
    })
  }
  nextMonth() {
    this.alertify.confirm("This will erase your current request ...",()=>{
      this.fm.makeVacant();
      this.sm.makeVacant();
      this.firstMonthRequestedDays = [];
      this.secondMonthRequestedDays = [];
      this.firstMonth.Id = this.firstMonth.Id + 1;
      this.secondMonth.Id = this.secondMonth.Id + 1;
  
      this.fm.nextMonth(this.firstMonth);
      this.sm.nextMonth(this.secondMonth);
    
    })
  }

  receiveUpdatesFirstMonth(dates: Array<string>) {
    this.firstMonthRequestedDays = [];
    for(let i=0;i<dates.length;i++){
    let help:requestDays = {daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 }
    this.gen.getUnitPrice(this.currentPicoUnitId,"PHP", help.date.getUTCDate(), help.date.getUTCMonth())
    .subscribe((next)=>{help.price = next}, (error)=>{this.alertify.error(error)}, ()=>{
      // do the rest when this observable is finished
      help.daynumber = +dates[i];
      help.month = this.currentMonth.toString();
      help.year = this.currentYear;
      help.date.setUTCFullYear(help.year);
      help.date.setUTCMonth(+help.month);
      help.date.setDate(help.daynumber);
      this.firstMonthRequestedDays.push(help);
     });
    }
  }

  receiveUpdatesSecondMonth(dates: Array<string>) {
    this.secondMonthRequestedDays = [];
    for(let i=0;i<dates.length;i++){
    let help:requestDays = {daynumber: 0, month: '', price: 0, year: 0, date: new Date, dayOfYear: 0 }
    this.gen.getUnitPrice(this.currentPicoUnitId,"PHP", help.date.getUTCDate(), help.date.getUTCMonth())
    .subscribe((next)=>{help.price = next}, (error)=>{this.alertify.error(error)}, ()=>{
      // do the rest when this observable is finished
      help.daynumber = +dates[i];
      help.month = this.currentMonth.toString();
      help.year = this.currentYear;
      help.date.setUTCFullYear(help.year);
      help.date.setUTCMonth(+help.month);
      help.date.setDate(help.daynumber);
      this.secondMonthRequestedDays.push(help);
     });
    }


   }

     

  getMonthText(test: number) { return this.gen.getMonthFromNo(test); }

 


}


function convertCurrency(price: number, selectedCurrency: string, desiredCurrency: string): number 
{
  let help = 0;

  return help;
}

