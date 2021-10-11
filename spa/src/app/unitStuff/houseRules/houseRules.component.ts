import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/Auth.service';
import { RequestedMonth } from 'src/app/_models/RequestedMonth';


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
    private router: Router, 
    private route:ActivatedRoute) { }

  ngOnInit() {
    
    this.picoUnit = +this.route.snapshot.params.id;
    if (this.picoUnit === 1) { 
      
      this.header = 'Myna 610-A';
      this.photo_map = "../../assets/images/unit-pictures/610-A/DSC_6774.JPG";
     };
     if (this.picoUnit === 2) { 
      
      this.header = 'Myna 611-A';
      this.photo_map = "../../assets/images/unit-pictures/610-A/DSC_6774.JPG";
     }
      

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


