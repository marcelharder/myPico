import { Component, OnInit, Input, AfterContentInit, Output, EventEmitter } from '@angular/core';

import { DaysService } from 'src/app/_services/days.service';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { OccupancyService } from 'src/app/_services/occupancy.service';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { GeneralService } from 'src/app/_services/general.service';
import { AuthService } from 'src/app/_services/Auth.service';

@Component({
  selector: 'app-first-month',
  templateUrl: './first-month.component.html',
  styleUrls: ['./first-month.component.css']
})
export class FirstMonthComponent implements AfterContentInit {
  @Output() updateDates = new EventEmitter<string[]>();
  requestedDates:Array<string> = [];
  requestedMonth:RequestedMonth = { appointmentId: 0, picoUnit: 0, year: 0, month: 0 };
  
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
    private auth: AuthService,
    private gen: GeneralService) { }

    ngAfterContentInit() {
    this.auth.firstMonth.subscribe((next)=>{
      this.requestedMonth = next;
      this.currentYear = this.requestedMonth.year; // write this to the html
      this.getOccDates(this.requestedMonth.picoUnit,this.requestedMonth.month, this.requestedMonth.year);
    })
  }

  nextMonth(m: RequestedMonth){
    //this.monthName = this.gen.getMonthFromNo(m.month);
    this.currentYear = m.year; // write this to the html
    this.getOccDates(m.picoUnit,m.month, m.year);
  }

  getOccDates(picoUnit: number,month: number, year: number) {
    this.dayService.getDays(picoUnit,month,year).subscribe((res) => {

      this.element_1 = this.decodeDateNumbers(res.dates[0]);
      this.element_2 = this.decodeDateNumbers(res.dates[1]);
      this.element_3 = this.decodeDateNumbers(res.dates[2]);
      this.element_4 = this.decodeDateNumbers(res.dates[3]);
      this.element_5 = this.decodeDateNumbers(res.dates[4]);
      this.element_6 = this.decodeDateNumbers(res.dates[5]);
      this.element_7 = this.decodeDateNumbers(res.dates[6]);
      this.element_8 = this.decodeDateNumbers(res.dates[7]);
      this.element_9 = this.decodeDateNumbers(res.dates[8]);
      this.element_10 = this.decodeDateNumbers(res.dates[9]);
      this.element_11 = this.decodeDateNumbers(res.dates[10]);
      this.element_12 = this.decodeDateNumbers(res.dates[11]);
      this.element_13 = this.decodeDateNumbers(res.dates[12]);
      this.element_14 = this.decodeDateNumbers(res.dates[13]);
      this.element_15 = this.decodeDateNumbers(res.dates[14]);
      this.element_16 = this.decodeDateNumbers(res.dates[15]);
      this.element_17 = this.decodeDateNumbers(res.dates[16]);
      this.element_18 = this.decodeDateNumbers(res.dates[17]);
      this.element_19 = this.decodeDateNumbers(res.dates[18]);
      this.element_20 = this.decodeDateNumbers(res.dates[19]);
      this.element_21 = this.decodeDateNumbers(res.dates[20]);
      this.element_22 = this.decodeDateNumbers(res.dates[21]);
      this.element_23 = this.decodeDateNumbers(res.dates[22]);
      this.element_24 = this.decodeDateNumbers(res.dates[23]);
      this.element_25 = this.decodeDateNumbers(res.dates[24]);
      this.element_26 = this.decodeDateNumbers(res.dates[25]);
      this.element_27 = this.decodeDateNumbers(res.dates[26]);
      this.element_28 = this.decodeDateNumbers(res.dates[27]);
      this.element_29 = this.decodeDateNumbers(res.dates[28]);
      this.element_30 = this.decodeDateNumbers(res.dates[29]);
      this.element_31 = this.decodeDateNumbers(res.dates[30]);
      this.element_32 = this.decodeDateNumbers(res.dates[31]);
      this.element_33 = this.decodeDateNumbers(res.dates[32]);
      this.element_34 = this.decodeDateNumbers(res.dates[33]);
      this.element_35 = this.decodeDateNumbers(res.dates[34]);
      this.element_36 = this.decodeDateNumbers(res.dates[35]);
      this.element_37 = this.decodeDateNumbers(res.dates[36]);
      this.element_38 = this.decodeDateNumbers(res.dates[37]);
      this.element_39 = this.decodeDateNumbers(res.dates[38]);
      this.element_40 = this.decodeDateNumbers(res.dates[39]);
      this.element_41 = this.decodeDateNumbers(res.dates[40]);
      this.element_42 = this.decodeDateNumbers(res.dates[41]);

      this.element_1_class = this.decodeColor(res.occupancy[0]);
      this.element_2_class = this.decodeColor(res.occupancy[1]);
      this.element_3_class = this.decodeColor(res.occupancy[2]);
      this.element_4_class = this.decodeColor(res.occupancy[3]);
      this.element_5_class = this.decodeColor(res.occupancy[4]);
      this.element_6_class = this.decodeColor(res.occupancy[5]);
      this.element_7_class = this.decodeColor(res.occupancy[6]);
      this.element_8_class = this.decodeColor(res.occupancy[7]);
      this.element_9_class = this.decodeColor(res.occupancy[8]);
      this.element_10_class = this.decodeColor(res.occupancy[9]);
      this.element_11_class = this.decodeColor(res.occupancy[10]);
      this.element_12_class = this.decodeColor(res.occupancy[11]);
      this.element_13_class = this.decodeColor(res.occupancy[12]);
      this.element_14_class = this.decodeColor(res.occupancy[13]);
      this.element_15_class = this.decodeColor(res.occupancy[14]);
      this.element_16_class = this.decodeColor(res.occupancy[15]);
      this.element_17_class = this.decodeColor(res.occupancy[16]);
      this.element_18_class = this.decodeColor(res.occupancy[17]);
      this.element_19_class = this.decodeColor(res.occupancy[18]);
      this.element_20_class = this.decodeColor(res.occupancy[19]);
      this.element_21_class = this.decodeColor(res.occupancy[20]);
      this.element_22_class = this.decodeColor(res.occupancy[21]);
      this.element_23_class = this.decodeColor(res.occupancy[22]);
      this.element_24_class = this.decodeColor(res.occupancy[23]);
      this.element_25_class = this.decodeColor(res.occupancy[24]);
      this.element_26_class = this.decodeColor(res.occupancy[25]);
      this.element_27_class = this.decodeColor(res.occupancy[26]);
      this.element_28_class = this.decodeColor(res.occupancy[27]);
      this.element_29_class = this.decodeColor(res.occupancy[28]);
      this.element_30_class = this.decodeColor(res.occupancy[29]);
      this.element_31_class = this.decodeColor(res.occupancy[30]);
      this.element_32_class = this.decodeColor(res.occupancy[31]);
      this.element_33_class = this.decodeColor(res.occupancy[32]);
      this.element_34_class = this.decodeColor(res.occupancy[33]);
      this.element_35_class = this.decodeColor(res.occupancy[34]);
      this.element_36_class = this.decodeColor(res.occupancy[35]);
      this.element_37_class = this.decodeColor(res.occupancy[36]);
      this.element_38_class = this.decodeColor(res.occupancy[37]);
      this.element_39_class = this.decodeColor(res.occupancy[39]);
      this.element_40_class = this.decodeColor(res.occupancy[39]);
      this.element_41_class = this.decodeColor(res.occupancy[40]);
      this.element_42_class = this.decodeColor(res.occupancy[41]);

      this.monthName = this.gen.getMonthFromNo(res.monthId);





      
      

    });

  }
 
  getDataForTable($event: any) {
    let id = 0;
    let value = "";
    id = $event.target.id;
    value = $event.target.value;
    if (value !== "") {
      if ($event.target.classList.contains('requested')) {
        $event.target.classList.remove('requested');
        $event.target.classList.add('vacant');
        var index: number = this.requestedDates.indexOf(value, 0);
        if (index > -1) { this.requestedDates.splice(index, 1); }
        this.updateDates.emit(this.requestedDates);

      } else {
        if ($event.target.classList.contains('vacant')) {
          $event.target.classList.remove('vacant');
          $event.target.classList.add('requested');
          this.requestedDates.push(value); // add the value to the list
          this.updateDates.emit(this.requestedDates);
        }
      }
    }
  }
  decodeColor(test: number): string {
    let help = "";
    switch (test) {
      case 3: return 'out_of_calendar'; break;
      case 2: return 'occupied'; break;
      case 0: return 'vacant'; break;
      case 1: return 'requested'; break;
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
  makeVacant() {
    this.requestedDates = [];
    const el = document.getElementsByClassName('requested');
    for (const x = 0; x < el.length; x) {
      el[x].className = 'vacant';
    }
    
  }
  

}
