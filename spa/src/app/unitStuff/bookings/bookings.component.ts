import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  currentPicoUnitId = 0;
  location="";
  firstMonth: RequestedMonth = { picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { picoUnit: 0, year: 0, month: 0 };

  //@ViewChild(FirstMonthComponent) fm!: FirstMonthComponent;

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
    // start with the current Month
    this.currentPicoUnitId = +this.route.snapshot.params.id;

    if(this.currentPicoUnitId === 1){this.location = "Myna 610-A"}
    if(this.currentPicoUnitId === 2){this.location = "Myna 611-A"}
    if(this.currentPicoUnitId === 3){this.location = "Myna 612-A"}
        
      
      this.firstMonth.picoUnit = this.currentPicoUnitId;
      this.firstMonth.year = 2018;
      this.firstMonth.month = 8;

      this.secondMonth.picoUnit = this.currentPicoUnitId;
      this.secondMonth.year = 2018;
      this.secondMonth.month = 9;
    

  }

  removeOccupancy(t: any) { }

  addOccupancy(t: any) { }

  prevMonth() {
    this.alertify.error("getting previous month");
    this.firstMonth.month = this.firstMonth.month - 1;
    this.secondMonth.month = this.secondMonth.month - 1;
  }
  nextMonth() {
    this.alertify.error("getting next month");
    this.secondMonth.month = this.secondMonth.month + 1;
    this.firstMonth.month = this.firstMonth.month + 1
  }



}
