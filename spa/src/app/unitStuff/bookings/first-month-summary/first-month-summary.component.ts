import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { requestDays } from 'src/app/_models/requestDays';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-monthSummary',
  templateUrl:'./first-month-summary.component.html',
  styleUrls: ['./first-month-summary.component.css']
})
export class MonthSummaryComponent implements OnInit {
  @Input() monthText: any;
  @Input() fmrd: Array<requestDays> = [];
  @Input() currentPicoUnitId = 0;
  monthName = "";
  
  desiredCurrency = 'PHP';
  constructor(private alertify: AlertifyService, private auth:AuthService, private gen: GeneralService, private router:Router) { }

  ngOnInit() {
    this.monthName = this.gen.getMonthFromNo(this.monthText).toString();

  }

  

  currencyChanged() {
    for (let i = 0; i < this.fmrd.length; i++) {
      let item: requestDays = this.fmrd[i];
      // get the price from the backend
      this.gen.getUnitPrice(this.currentPicoUnitId, this.desiredCurrency, item.daynumber, +item.month).subscribe((next)=>{
        item.price = next;
      })
    }

    this.alertify.success(this.desiredCurrency);
  }

  RequestStay(){
    // show the email page with the email text and appointment details do this all on the server
    // then make a sort of invoice page
    this.alertify.success("Request stay");}
  
  Cancel(){
    
    this.router.navigate(['']);
  
  }

}
