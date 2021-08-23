import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
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
  firstMonth!: RequestedMonth;
  secondMonth!: RequestedMonth;

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
    private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {

    if (!this.auth.loggedIn()) {
      debugger;
      this.alertify.error("Please log in ...");
      this.router.navigate(['/login']);

    } else {
      debugger;
      // start with the current Month
      this.auth.currentPicoUnit.subscribe((next) => {
        let help = next;
        this.gen.getPicoUnitId(help).subscribe((next) => {
          this.firstMonth.picoUnit = next;
          this.firstMonth.year = 2018;
          this.firstMonth.month = 8;
          this.secondMonth.picoUnit = next;
          this.secondMonth.year = 2018;
          this.secondMonth.month = 9;
        })
      });
    }









  }

  removeOccupancy(t: any) { }

  addOccupancy(t: any) { }



}
