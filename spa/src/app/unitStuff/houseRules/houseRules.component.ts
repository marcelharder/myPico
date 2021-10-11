import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/Auth.service';

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
    // go to the booking page
    this.router.navigate(['/unitBookings/' + this.picoUnit]);
  }
  showPHP() { };
  showUSD() { };

}
