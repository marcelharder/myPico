import { Component, OnInit, Input } from '@angular/core';

import { DaysService } from 'src/app/_services/days.service';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { OccupancyService } from 'src/app/_services/occupancy.service';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-first-month',
  templateUrl: './first-month.component.html',
  styleUrls: ['./first-month.component.css']
})
export class FirstMonthComponent implements OnInit {
  @Input() rm: RequestedMonth | undefined
  currentMonth = 0;
  currentYear = 0;
  monthName: String = "";
  currentPicoUnitId = 0;
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

  constructor(
    private dayService: DaysService,
    private alertify: AlertifyService,
    private gen: GeneralService,
    private occupancyService: OccupancyService) { }

  ngOnInit() {
    if (this.rm !== undefined) {
      this.getOccDates(this.rm.Id);
      this.getOccupancy(this.rm.picoUnit,this.rm.Id);
      }
  }

  nextMonth(m: RequestedMonth){
    this.getOccDates(m.Id);
    this.getOccupancy(m.picoUnit,m.Id);
  }

  getOccDates(id: number) {

    this.dayService.getDays(id).subscribe((res) => {

      this.element_1 = this.decodeDateNumbers(res.day_1);
      this.element_2 = this.decodeDateNumbers(res.day_2);
      this.element_3 = this.decodeDateNumbers(res.day_3);
      this.element_4 = this.decodeDateNumbers(res.day_4);
      this.element_5 = this.decodeDateNumbers(res.day_5);
      this.element_6 = this.decodeDateNumbers(res.day_6);
      this.element_7 = this.decodeDateNumbers(res.day_7);
      this.element_8 = this.decodeDateNumbers(res.day_8);
      this.element_9 = this.decodeDateNumbers(res.day_9);
      this.element_10 = this.decodeDateNumbers(res.day_10);
      this.element_11 = this.decodeDateNumbers(res.day_11);
      this.element_12 = this.decodeDateNumbers(res.day_12);
      this.element_13 = this.decodeDateNumbers(res.day_13);
      this.element_14 = this.decodeDateNumbers(res.day_14);
      this.element_15 = this.decodeDateNumbers(res.day_15);
      this.element_16 = this.decodeDateNumbers(res.day_16);
      this.element_17 = this.decodeDateNumbers(res.day_17);
      this.element_18 = this.decodeDateNumbers(res.day_18);
      this.element_19 = this.decodeDateNumbers(res.day_19);
      this.element_20 = this.decodeDateNumbers(res.day_20);
      this.element_21 = this.decodeDateNumbers(res.day_21);
      this.element_22 = this.decodeDateNumbers(res.day_22);
      this.element_23 = this.decodeDateNumbers(res.day_23);
      this.element_24 = this.decodeDateNumbers(res.day_24);
      this.element_25 = this.decodeDateNumbers(res.day_25);
      this.element_26 = this.decodeDateNumbers(res.day_26);
      this.element_27 = this.decodeDateNumbers(res.day_27);
      this.element_28 = this.decodeDateNumbers(res.day_28);
      this.element_29 = this.decodeDateNumbers(res.day_29);
      this.element_30 = this.decodeDateNumbers(res.day_30);
      this.element_31 = this.decodeDateNumbers(res.day_31);
      this.element_32 = this.decodeDateNumbers(res.day_32);
      this.element_33 = this.decodeDateNumbers(res.day_33);
      this.element_34 = this.decodeDateNumbers(res.day_34);
      this.element_35 = this.decodeDateNumbers(res.day_35);
      this.element_36 = this.decodeDateNumbers(res.day_36);
      this.element_37 = this.decodeDateNumbers(res.day_37);
      this.element_38 = this.decodeDateNumbers(res.day_38);
      this.element_39 = this.decodeDateNumbers(res.day_39);
      this.element_40 = this.decodeDateNumbers(res.day_40);
      this.element_41 = this.decodeDateNumbers(res.day_41);
      this.element_42 = this.decodeDateNumbers(res.day_42);
      this.monthName = this.gen.getMonthFromNo(res.MonthId);
      this.currentYear = res.Year;

    });

  }
  getOccupancy(unit: number, id: number) {
    this.occupancyService.getOccupancy(unit, id).subscribe((res) => {
      this.element_1_class = this.decodeColor(res.day_1);
      this.element_2_class = this.decodeColor(res.day_2);
      this.element_3_class = this.decodeColor(res.day_3);
      this.element_4_class = this.decodeColor(res.day_4);
      this.element_5_class = this.decodeColor(res.day_5);
      this.element_6_class = this.decodeColor(res.day_6);
      this.element_7_class = this.decodeColor(res.day_7);
      this.element_8_class = this.decodeColor(res.day_8);
      this.element_9_class = this.decodeColor(res.day_9);
      this.element_10_class = this.decodeColor(res.day_10);
      this.element_11_class = this.decodeColor(res.day_11);
      this.element_12_class = this.decodeColor(res.day_12);
      this.element_13_class = this.decodeColor(res.day_13);
      this.element_14_class = this.decodeColor(res.day_14);
      this.element_15_class = this.decodeColor(res.day_15);
      this.element_16_class = this.decodeColor(res.day_16);
      this.element_17_class = this.decodeColor(res.day_17);
      this.element_18_class = this.decodeColor(res.day_18);
      this.element_19_class = this.decodeColor(res.day_19);
      this.element_20_class = this.decodeColor(res.day_20);
      this.element_21_class = this.decodeColor(res.day_21);
      this.element_22_class = this.decodeColor(res.day_22);
      this.element_23_class = this.decodeColor(res.day_23);
      this.element_24_class = this.decodeColor(res.day_24);
      this.element_25_class = this.decodeColor(res.day_25);
      this.element_26_class = this.decodeColor(res.day_26);
      this.element_27_class = this.decodeColor(res.day_27);
      this.element_28_class = this.decodeColor(res.day_28);
      this.element_29_class = this.decodeColor(res.day_29);
      this.element_30_class = this.decodeColor(res.day_30);
      this.element_31_class = this.decodeColor(res.day_31);
      this.element_32_class = this.decodeColor(res.day_32);
      this.element_33_class = this.decodeColor(res.day_33);
      this.element_34_class = this.decodeColor(res.day_34);
      this.element_35_class = this.decodeColor(res.day_35);
      this.element_36_class = this.decodeColor(res.day_36);
      this.element_37_class = this.decodeColor(res.day_37);
      this.element_38_class = this.decodeColor(res.day_38);
      this.element_39_class = this.decodeColor(res.day_39);
      this.element_40_class = this.decodeColor(res.day_40);
      this.element_41_class = this.decodeColor(res.day_41);
      this.element_42_class = this.decodeColor(res.day_42);
    });
  }
  getDataForTable($event: any) {
    let id = 0;
    let value = "";
    id = $event.target.id;
    value = $event.target.value;
    if (value !== "") {
      if ($event.target.classList.contains('requested')) {
        //  this.removeOccupancy.emit((this.currentMonth) + '/' + $event.target.value + '/' + this.rm.year);

        $event.target.classList.remove('requested');
        $event.target.classList.add('vacant');

      } else {
        if ($event.target.classList.contains('vacant')) {
          //  this.addOccupancy.emit((this.currentMonth) + '/' + $event.target.value + '/' + this.rm.year);

          $event.target.classList.remove('vacant');
          $event.target.classList.add('requested');

        }
      }
    }
  }
  decodeColor(test: number): string {
    let help = "";
    switch (test) {
      case 3: return 'out_of_calendar'; break;
      case 1: return 'occupied'; break;
      case 0: return 'vacant'; break;
      case 2: return 'requested'; break;
    }
    return help;
  }
  decodeDateNumbers(test: number): string{
    let help = "";
    switch (test) {
      case 0: return ''; break;
      default: help = test.toString();break;
    }
    return help;

  }
  

}
