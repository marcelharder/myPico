import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Appointment } from 'src/app/_models/appointment';
import { Message } from 'src/app/_models/Message';
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

  mes: Message = {
    id:0,senderId:0,senderKnownAs:'',
    senderPhotoUrl:'',recipientId:0,
    recipientKnownAs:'',recipientPhotoUrl:'',
    content:'',isRead:false,dateRead: new Date, messageSent: new Date };
  appt: Appointment = {
      picoUnitId: 0,
      picoUnitPhotoUrl: "",
      userId: 0,
      requestedDays: [],
      startDate: new Date,
      endDate: new Date,
      noOfNights: 0,
      id: 0,
      year: 0,
      month: 0,
      day: 0,
      status: "",
      rent: 0,
      rentUSD: 0,
      downPayment: 0,
      paid_InFull: 0,
      comment: ""};
    
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
    this.alertify.success('stay requested in Unit ' + this.currentPicoUnitId);
    // send a message to the caretaker of this unit ....
   
  
  }
  
  Cancel(){
    
    this.router.navigate(['']);
  
  }

}
