import { Component, OnInit, ViewChild } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Appointment } from 'src/app/_models/appointment';
import { DaysModel } from 'src/app/_models/daysModel';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { User } from 'src/app/_models/user';
import { Utilities } from 'src/app/_services/utilities';
import { FirstMonthComponent } from './first-month/first-month.component';
import { SecondMonthComponent } from './second-month/second-month.component';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  @ViewChild(FirstMonthComponent) fm!: FirstMonthComponent;
  @ViewChild(SecondMonthComponent) sm!: SecondMonthComponent;
  firstMonth!: RequestedMonth;
  secondMonth!: RequestedMonth;
  listDaysArray: Array<DaysModel> = [];
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
  allowDeletingOccupancy = false;

  constructor() { }

  ngOnInit() {
  }

  removeOccupancy(){}
  
  addOccupancy(t:any){}



}
