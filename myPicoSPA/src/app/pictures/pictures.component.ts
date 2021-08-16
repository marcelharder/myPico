import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-pictures',
  templateUrl: './pictures.component.html',
  styleUrls: ['./pictures.component.css']
})
export class PicturesComponent implements OnInit {
  picoUnit = 0;
  pictureLocation = "Pico de loro";

  photo_1: string;
  photo_2: string;
  photo_3: string;
  photo_4: string;
  photo_5: string;
  photo_6: string;
  photo_7: string;
  photo_8: string;
  photo_9: string;
  photo_10: string;

  constructor(
    private auth: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {

     this.picoUnit = +this.route.snapshot.paramMap.get('id');

    
    if (this.picoUnit === 1) {
      this.showDetails(1);
      this.pictureLocation = "Unit 610-A";
      this.photo_1 = "../../assets/images/unit-pictures/610-A/DSC_6765.JPG";
      this.photo_2 = "../../assets/images/unit-pictures/610-A/DSC_6770.JPG";
      this.photo_3 = "../../assets/images/unit-pictures/610-A/DSC_6771.JPG";
      this.photo_4 = "../../assets/images/unit-pictures/610-A/DSC_6772.JPG";
      this.photo_5 = "../../assets/images/unit-pictures/610-A/DSC_6773.JPG";
      this.photo_6 = "../../assets/images/unit-pictures/610-A/DSC_6774.JPG";
      this.photo_7 = "../../assets/images/unit-pictures/610-A/DSC_6775.JPG";
      this.photo_8 = "../../assets/images/unit-pictures/610-A/DSC_6777.JPG";
      this.photo_9 = "../../assets/images/unit-pictures/610-A/DSC_6778.JPG";

    }
    if (this.picoUnit === 2) {
      this.showDetails(1);
      this.pictureLocation = "Unit 620-A";

    }
    if (this.picoUnit === 3) {
      this.showDetails(1);
      this.pictureLocation = "Unit 640-A";

    }
  }
  showDetails(x)  { if (this.picoUnit === 0) {return false; } else {return true; } }

  availability() {
    if (this.auth.loggedIn()) {
     this.router.navigate(['/schedule']);
    } else {this.router.navigate(['/login']); }
    }
  details() {this.router.navigate(['/unitDetails/' + this.picoUnit]); }
}
