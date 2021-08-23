import { Component, OnInit, Input, Output, EventEmitter, AfterContentInit } from '@angular/core';

import { Utilities } from 'src/app/_services/utilities';
import { DaysService } from 'src/app/_services/days.service';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { OccupancyService } from 'src/app/_services/occupancy.service';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-first-month',
  templateUrl: './first-month.component.html',
  styleUrls: ['./first-month.component.css']
})
export class FirstMonthComponent implements OnInit {
  @Input() rm: RequestedMonth | undefined
  currentMonth = 0;
  monthName:String = "";
  currentPicoUnitId = 0;


 /*  @Input() obs = false; // one beyond sequence, makes sure that we add requested days as a continuous block
  @Input() bordersequence = false; // border sequence, makes sure that we can only delete the 'eindstandige' days

  @Output() addOccupancy = new EventEmitter();
  @Output() removeOccupancy = new EventEmitter();
  currentMonth = 0;
  

  status = "";

  monthName = "";
  listOccupancy: Array<string> = [];
  listSelectedIds: Array<number> = []; */

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
    private gen:GeneralService,
    private occupancyService: OccupancyService, 
    private alert: AlertifyService) { }

   ngOnInit() {
     if(this.rm !== undefined){
      this.currentMonth = this.rm.month;
      this.monthName = this.gen.getMonthFromNo(this.currentMonth);
      this.getOccDates(this.rm.year, this.rm.month);
      this.getOccupancy(this.rm.picoUnit, this.rm.year, this.rm.month);
     }
    
    
   
 
  //  this.bordersequence = false;
  //  this.obs = false;
  }

 /*  callFromAbove() {
    this.currentMonth = this.rm.month;
    const util = new Utilities();
    this.monthName = util.translateMonth(this.currentMonth - 1);
    this.getOccDates(this.rm.year, this.currentMonth);
  //  this.getOccupancy(this.rm.year, this.currentMonth);
   }
 */
  getOccDates(year: number, month: number) {

      this.dayService.getDays(year, month).subscribe((res) => {
     
      this.element_1 = res.day_1; this.element_2 = res[1]; this.element_3 = res[2]; 
      this.element_4 = res[3]; this.element_5 = res[4];
      debugger;
      this.element_6 = res[5]; this.element_7 = res[6]; this.element_8 = res[7]; 
      this.element_9 = res[8]; this.element_10 = res[9];
      this.element_11 = res[10]; this.element_12 = res[11]; this.element_13 = res[12]; 
      this.element_14 = res[13]; this.element_15 = res[14];
      this.element_16 = res[15]; this.element_17 = res[16]; this.element_18 = res[17]; 
      this.element_19 = res[18]; this.element_20 = res[19];
      this.element_21 = res[20]; this.element_22 = res[21]; this.element_23 = res[22]; 
      this.element_24 = res[23]; this.element_25 = res[24];
      this.element_26 = res[25]; this.element_27 = res[26]; this.element_28 = res[27]; 
      this.element_29 = res[28]; this.element_30 = res[29];
      this.element_31 = res[30]; this.element_32 = res[31]; this.element_33 = res[32]; 
      this.element_34 = res[33]; this.element_35 = res[34];
      this.element_36 = res[35]; this.element_37 = res[36]; this.element_38 = res[37]; 
      this.element_39 = res[38]; this.element_40 = res[39];
      this.element_41 = res[40]; this.element_42 = res[41];
    }); 

  }
  getOccupancy(unit: number,year: number, month: number) {
     this.occupancyService.getOccupancy(unit, year, month).subscribe((res) => {
      this.element_1_class = res[0]; this.element_2_class = res[1]; 
      this.element_3_class = res[2]; this.element_4_class = res[3]; 
      this.element_5_class = res[4];
      this.element_6_class = res[5]; this.element_7_class = res[6]; 
      this.element_8_class = res[7]; this.element_9_class = res[8]; 
      this.element_10_class = res[9];
      this.element_11_class = res[10]; this.element_12_class = res[11]; 
      this.element_13_class = res[12]; this.element_14_class = res[13]; 
      this.element_15_class = res[14];
      this.element_16_class = res[15]; this.element_17_class = res[16]; 
      this.element_18_class = res[17]; this.element_19_class = res[18]; 
      this.element_20_class = res[19];
      this.element_21_class = res[20]; this.element_22_class = res[21]; 
      this.element_23_class = res[22]; this.element_24_class = res[23]; 
      this.element_25_class = res[24];
      this.element_26_class = res[25]; this.element_27_class = res[26]; 
      this.element_28_class = res[27]; this.element_29_class = res[28]; 
      this.element_30_class = res[29];
      this.element_31_class = res[30]; this.element_32_class = res[31]; 
      this.element_33_class = res[32]; this.element_34_class = res[33]; 
      this.element_35_class = res[34];
      this.element_36_class = res[35]; this.element_37_class = res[36]; 
      this.element_38_class = res[37]; this.element_39_class = res[38]; 
      this.element_40_class = res[39];
      this.element_41_class = res[40]; this.element_42_class = res[41];
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


/* makeVacant() {
  const el = document.getElementsByClassName('requested');
  for (const x = 0; x < el.length; x) {
    el[x].className = 'vacant';
  }
  this.listSelectedIds = [];
} */

}
