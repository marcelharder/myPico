import { Component, OnInit, Input, Output, EventEmitter, AfterContentInit } from '@angular/core';

import { Utilities } from '../../_helpers/utilities';
import { DaysService } from '../../_services/days.service';
import 'rxjs/add/operator/first';
import { RequestedMonth } from '../../_models/RequestedMonth';
import { OccupancyService } from '../../_services/occupancy.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-second-month',
  templateUrl: './second-month.component.html',
  styleUrls: ['./second-month.component.css']
})
export class SecondMonthComponent implements AfterContentInit {
  @Input() rm: RequestedMonth;
  @Input() obs: boolean; // one beyond sequence, makes sure that we add requested days as a continuous block
  @Input() bordersequence: boolean; // border sequence, makes sure that we can only delete the 'eindstandige' days
  @Output() addOccupancy = new EventEmitter();
  @Output() removeOccupancy = new EventEmitter();
  currentMonth = 0;


  monthName = "";
  listOccupancy: Array<string> = [];
  listSelectedIds: Array<number> = [];

  element_1_class = 'vacant'; element_2_class = 'vacant'; element_3_class = 'vacant'; element_4_class = 'vacant'; element_5_class = 'vacant';
  element_6_class = 'vacant'; element_7_class = 'vacant'; element_8_class = 'vacant'; element_9_class = 'vacant'; element_10_class = 'vacant';
  element_11_class = 'vacant'; element_12_class = 'vacant'; element_13_class = 'vacant'; element_14_class = 'vacant'; element_15_class = 'vacant';
  element_16_class = 'vacant'; element_17_class = 'vacant'; element_18_class = 'vacant'; element_19_class = 'vacant'; element_20_class = 'vacant';
  element_21_class = 'vacant'; element_22_class = 'vacant'; element_23_class = 'vacant'; element_24_class = 'vacant'; element_25_class = 'vacant';
  element_26_class = 'vacant'; element_27_class = 'vacant'; element_28_class = 'vacant'; element_29_class = 'vacant'; element_30_class = 'vacant';
  element_31_class = 'vacant'; element_32_class = 'vacant'; element_33_class = 'vacant'; element_34_class = 'vacant'; element_35_class = 'vacant';
  element_36_class = 'vacant'; element_37_class = 'vacant'; element_38_class = 'vacant'; element_39_class = 'vacant'; element_40_class = 'vacant';
  element_41_class = 'vacant'; element_42_class = 'vacant';

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
    private occupancyService: OccupancyService,
    private alert: AlertifyService) { }

  ngAfterContentInit() {
    this.currentMonth = this.rm.month;
    this.bordersequence = false;
    this.obs = false;

    /* const util = new Utilities();
    this.monthName = util.translateMonth(this.currentMonth);
    this.getOccDates(this.rm.year, this.currentMonth);
    this.getOccupancy(this.rm.picoUnit, this.rm.year, this.currentMonth); */
  }

  callFromAbove() {
    this.currentMonth = this.rm.month;
    const util = new Utilities();
    this.monthName = util.translateMonth(this.currentMonth - 1);
    this.getOccDates(this.rm.year, this.currentMonth);
    this.getOccupancy(this.rm.picoUnit, this.rm.year, this.currentMonth);
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
    this.occupancyService.getOccupancy(Id, year, month).subscribe((res) => {
      this.element_1_class = res.day_1; this.element_2_class = res.day_2; this.element_3_class = res.day_3; this.element_4_class = res.day_4; this.element_5_class = res.day_5;
      this.element_6_class = res.day_6; this.element_7_class = res.day_7; this.element_8_class = res.day_8; this.element_9_class = res.day_9; this.element_10_class = res.day_10;
      this.element_11_class = res.day_11; this.element_12_class = res.day_12; this.element_13_class = res.day_13; this.element_14_class = res.day_14; this.element_15_class = res.day_15;
      this.element_16_class = res.day_16; this.element_17_class = res.day_17; this.element_18_class = res.day_18; this.element_19_class = res.day_19; this.element_20_class = res.day_20;
      this.element_21_class = res.day_21; this.element_22_class = res.day_22; this.element_23_class = res.day_23; this.element_24_class = res.day_24; this.element_25_class = res.day_25;
      this.element_26_class = res.day_26; this.element_27_class = res.day_27; this.element_28_class = res.day_28; this.element_29_class = res.day_29; this.element_30_class = res.day_30;
      this.element_31_class = res.day_31; this.element_32_class = res.day_32; this.element_33_class = res.day_33; this.element_34_class = res.day_34; this.element_35_class = res.day_35;
      this.element_36_class = res.day_36; this.element_37_class = res.day_37; this.element_38_class = res.day_38; this.element_39_class = res.day_39; this.element_40_class = res.day_40;
      this.element_41_class = res.day_41; this.element_42_class = res.day_42;
    });
  }
  makeVacant() {
    const el = document.getElementsByClassName('requested');
    for (let x = 0; x < el.length; x) {
      el[x].className = 'vacant';
    }
  }

  getDataForTable($event: any) {
    let id = 0;
    let value = "";
    id = $event.target.id;
    value = $event.target.value;
    if (value !== "") {
      if ($event.target.classList.contains('requested')) {
        this.removeOccupancy.emit((this.currentMonth) + '/' + $event.target.value + '/' + this.rm.year);
        if (this.bordersequence) {
          $event.target.classList.remove('requested');
          $event.target.classList.add('vacant');
        }
      } else {
        if ($event.target.classList.contains('vacant')) {
          this.addOccupancy.emit((this.currentMonth) + '/' + $event.target.value + '/' + this.rm.year);
          if (this.obs) {
            $event.target.classList.remove('vacant');
            $event.target.classList.add('requested');
          }
        }
      }
    }
  }

}

