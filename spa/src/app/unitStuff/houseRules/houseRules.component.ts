import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/Auth.service';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';
import { GeneralService } from 'src/app/_services/general.service';


@Component({
  selector: 'app-houseRules',
  templateUrl: './houseRules.component.html',
  styleUrls: ['./houseRules.component.css']
})
export class HouseRulesComponent implements OnInit {
  picoUnit = 0;
  header = '';
  photo_map = "";
  currency = "PHP";
  currentYear = 0;
  currentMonth = 0;
  firstMonth: RequestedMonth = { appointmentId: 0, picoUnit: 0, year: 0, month: 0 };
  secondMonth: RequestedMonth = { appointmentId: 0, picoUnit: 0, year: 0, month: 0 };

  constructor(private auth: AuthService, 
    private gen:GeneralService,
    private router: Router, 
    private route:ActivatedRoute) { }

  ngOnInit() {
   
    this.auth.currentPicoUnit.subscribe((next)=>{
      this.picoUnit = +next;
      debugger;
      // get the unit details
      this.gen.getPicoUnitDetails(this.picoUnit).subscribe((next)=>{
        this.header = next.picoUnitNumber;
        this.photo_map == next.main_Photo_Url;
      })
    })
  }

  availability() {
    let dateTime = new Date();
    // zet het jaar op het huidige jaar
    this.currentYear = dateTime.getFullYear();
    // zet de maand op de huidige maand
    this.currentMonth = dateTime.getMonth();

    // save some stuff to the behaviorSubject
    this.firstMonth.appointmentId = 0;
    this.firstMonth.picoUnit = this.picoUnit;
   
    this.secondMonth.appointmentId = 0;
    this.secondMonth.picoUnit = this.picoUnit;

    if(this.secondMonth.month === 1){
      this.firstMonth.year = this.currentYear - 1;
      this.firstMonth.month = 11;
    } else{
      this.firstMonth.year = this.currentYear;
      this.firstMonth.month = this.currentMonth;
    }
    
    if(this.firstMonth.month === 11){
      this.secondMonth.year = this.currentYear + 1;
      this.secondMonth.month = 0;
    } else{
      this.secondMonth.year = this.currentYear;
      this.secondMonth.month = this.currentMonth + 1;
    }
   

    this.auth.setFirstMonth(this.firstMonth);
    this.auth.setSecondMonth(this.secondMonth);

    // go to the booking page
    this.router.navigate(['/unitBookings/' + this.picoUnit]);
  }
  showPHP() { };
  showUSD() { };

}


