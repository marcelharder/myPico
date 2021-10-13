import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { requestDays } from 'src/app/_models/requestDays';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-second-month-summary',
  templateUrl: './second-month-summary.component.html',
  styleUrls: ['./second-month-summary.component.css']
})
export class SecondMonthSummaryComponent implements OnInit {

  @Input() monthText: any;
  @Input() fmrd: Array<requestDays> = [];
  @Input() currentPicoUnitId = 0;
  monthName = "";
  
  desiredCurrency = 'PHP';
  constructor(private alertify: AlertifyService, private auth:AuthService, private gen: GeneralService, private router: Router) { }

  ngOnInit() {
    this.monthName = this.gen.getMonthFromNo(this.monthText).toString();
  }

  

  RequestStay(){this.alertify.success('stay requested');}
  Cancel(){this.router.navigate(['']);}

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
}
