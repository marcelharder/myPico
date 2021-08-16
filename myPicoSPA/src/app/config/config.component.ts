import { PicoUnitService } from './../_services/pico-unit.service';
import { SeasonDays } from './../_models/seasonDays';
import { SeasonService } from './../_services/season.service';
import { UserService } from './../_services/user.service';
import { DaysService } from './../_services/days.service';
import { Component, OnInit } from '@angular/core';
import { Utilities } from '../_helpers/utilities';
import { PicoUnit } from '../_models/PicoUnit';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';

@Component({

  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent implements OnInit {
  util: Utilities = new Utilities();
  season: SeasonDays = new SeasonDays();
  selectedAppartmentUser = "";
  appartmentUsers: Array<User> = [];
  picUnit: PicoUnit;
  currentuser: string;
  displaySeason = 0;
  displayExtra_01 = 0;
  displayExtra_02 = 0;
  hmos: Array<string> = this.util.getMonths();
  years: Array<string> = this.util.getYears();
  headingYear = "";
  headingMonth = "";
  selectedYear = "2018";
  selectedMonth = "January";
  selectedLowSeasonPrice = 0;
  selectedMidSeasonPrice = 0;
  selectedHighSeasonPrice = 0;
  priceLowSeason: Array<number> = this.util.getSeasonPrice(1);
  priceMidSeason: Array<number> = this.util.getSeasonPrice(2);
  priceHighSeason: Array<number> = this.util.getSeasonPrice(3);

  element_1 = ''; element_2 = ''; element_3 = ''; element_4 = ''; element_5 = '';
  element_6 = ''; element_7 = ''; element_8 = ''; element_9 = ''; element_10 = '';
  element_11 = ''; element_12 = ''; element_13 = ''; element_14 = ''; element_15 = '';
  element_16 = ''; element_17 = ''; element_18 = ''; element_19 = ''; element_20 = '';
  element_21 = ''; element_22 = ''; element_23 = ''; element_24 = ''; element_25 = '';
  element_26 = ''; element_27 = ''; element_28 = ''; element_29 = ''; element_30 = '';
  element_31 = ''; element_32 = ''; element_33 = ''; element_34 = ''; element_35 = '';
  element_36 = ''; element_37 = ''; element_38 = ''; element_39 = ''; element_40 = '';
  element_41 = ''; element_42 = '';



  constructor(private dayService: DaysService,
    private sService: SeasonService,
    private us: UserService,
    private pus: PicoUnitService,
    private auth: AuthService) { }

  ngOnInit() {


    this.headingMonth = this.selectedMonth;
    this.headingYear = this.selectedYear;
    // get the appartment from the current user
    this.pus.getPicoUnitManagedByThisUser(this.auth.currentUser.id).subscribe((p) => { this.picUnit = p; });
    this.us.getAppartmentUsers(parseInt(this.picUnit.id, 2), this.auth.currentUser.id).subscribe((p) => { this.appartmentUsers = p; });
    this.selectedAppartmentUser = this.appartmentUsers[0].knownAs;
    
    // get the season days from the service


  }
  saveUserDetails() { alert("Saving user details"); }
  saveRent() { this.pus.saveSeasonPrices(this.auth.currentUser.id, this.picUnit); }

  saveSeason() {this.sService.sendSeasonDays(this.auth.currentUser.id, this.season);  }


  showSeason() {
    // get the season stuff
    this.displaySeason = 1; this.displayExtra_01 = 0; this.displayExtra_02 = 0;
  }
  showPricing() {
    // get the pricing stuff
    this.displaySeason = 0; this.displayExtra_01 = 1; this.displayExtra_02 = 0;
    
    }
  showExtra_02() {
    this.displaySeason = 0; this.displayExtra_01 = 0; this.displayExtra_02 = 1;
  }

  showSelectedMonth() {
    this.headingMonth = this.selectedMonth;
    this.headingYear = this.selectedYear;
    this.getOccDates(+this.selectedYear, this.util.getNumberOfMonth(this.selectedMonth));
    this.getOccupancy(parseInt(this.picUnit.id, 2), +this.selectedYear, this.util.getNumberOfMonth(this.selectedMonth));
  }

  getOccDates(year: number, month: number) {
    this.dayService.getDays(year, month).subscribe((res) => {
      this.element_1 = res.day_1; this.element_2 = res.day_2; this.element_3 = res.day_3; this.element_4 = res.day_4; this.element_5 = res.day_5;
      this.element_6 = res.day_6; this.element_7 = res.day_7; this.element_8 = res.day_8; this.element_9 = res.day_9; this.element_10 = res.day_10;
      this.element_11 = res.day_11; this.element_12 = res.day_12; this.element_13 = res.day_13; this.element_14 = res.day_14; this.element_15 = res.day_15;
      this.element_16 = res.day_16; this.element_17 = res.day_17; this.element_18 = res.day_18; this.element_19 = res.day_19; this.element_20 = res.day_20;
      this.element_21 = res.day_21; this.element_22 = res.day_22; this.element_23 = res.day_23; this.element_24 = res.day_24; this.element_25 = res.day_25;
      this.element_26 = res.day_26; this.element_27 = res.day_27; this.element_28 = res.day_28; this.element_29 = res.day_29; this.element_30 = res.day_30;
      this.element_31 = res.day_31; this.element_32 = res.day_32; this.element_33 = res.day_33; this.element_34 = res.day_34; this.element_35 = res.day_35;
      this.element_36 = res.day_36; this.element_37 = res.day_37; this.element_38 = res.day_38; this.element_39 = res.day_39; this.element_40 = res.day_40;
      this.element_41 = res.day_41; this.element_42 = res.day_42;
    });

  }

  getOccupancy(Id: number, year: number, month: number) {
    this.sService.getSeasonDays(Id, year, month).subscribe((res) => {   this.season = res;   });
  }

  currentAppartment() {
    return this.picUnit.picoUnitNumber;
  }

  getDataForTable($event: any) {
    let id = "0";
    let value = "";
    id = $event.target.id;
    value = $event.target.value;

    if (value !== "") {
      if ($event.target.classList.contains('low')) {
        const util = new Utilities();
        // this.addOccupancy.emit((this.selectedMonth) + '/' + $event.target.value + '/' + this.rm.year);
        $event.target.classList.remove('low');
        $event.target.classList.add('mid');
         this.season = this.util.saveSeason(this.season, +id, "mid");
      } else {
        if ($event.target.classList.contains('mid')) {
          const util = new Utilities();
          $event.target.classList.remove('mid');
          $event.target.classList.add('high');
         this.season = this.util.saveSeason(this.season, +id, "high");

        } else {
          if ($event.target.classList.contains('high')) {
            const util = new Utilities();
            $event.target.classList.remove('high');
            $event.target.classList.add('low');
             this.season = this.util.saveSeason(this.season, +id, "low");
          }
        }
      }
    }
  }


}
