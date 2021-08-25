import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../_services/Auth.service';

@Component({
  selector: 'app-unitPictures',
  templateUrl: './unitPictures.component.html',
  styleUrls: ['./unitPictures.component.css']
})
export class UnitPicturesComponent implements OnInit {
  currentPicoUnitId = 0;;
  pictureLocation = "Pico de loro";

  photo_1: string = "";
  photo_2: string = "";
  photo_3: string = "";
  photo_4: string = "";
  photo_5: string = "";
  photo_6: string = "";
  photo_7: string = "";
  photo_8: string = "";
  photo_9: string = "";
  photo_10: string = "";

  constructor(private route:ActivatedRoute, 
    private auth:AuthService, 
    private router:Router) { }

  ngOnInit() {
    this.currentPicoUnitId = +this.route.snapshot.params.id;
    
       
    if (this.currentPicoUnitId === 1) {
      this.showDetails(1);
      this.pictureLocation = "Unit 610-A";
      this.photo_1 = "../assets/images/unit-pictures/610-A/DSC_6765.JPG";
      this.photo_2 = "../assets/images/unit-pictures/610-A/DSC_6770.JPG";
      this.photo_3 = "../assets/images/unit-pictures/610-A/DSC_6771.JPG";
      this.photo_4 = "../assets/images/unit-pictures/610-A/DSC_6772.JPG";
      this.photo_5 = "../assets/images/unit-pictures/610-A/DSC_6773.JPG";
      this.photo_6 = "../assets/images/unit-pictures/610-A/DSC_6774.JPG";
      this.photo_7 = "../assets/images/unit-pictures/610-A/DSC_6775.JPG";
      this.photo_8 = "../assets/images/unit-pictures/610-A/DSC_6776.JPG";
      this.photo_9 = "../assets/images/unit-pictures/610-A/DSC_6777.JPG";
      this.photo_10 = "../assets/images/unit-pictures/610-A/DSC_6778.JPG";

    }
    if (this.currentPicoUnitId === 2) {
      this.showDetails(1);
      this.pictureLocation = "Unit 620-A";

    }
    if (this.currentPicoUnitId === 3) {
      this.showDetails(1);
      this.pictureLocation = "Unit 640-A";

    }
   

  }
  showDetails(x:number)  { if (this.currentPicoUnitId === 0) {return false; } else {return true; } }

  availability() {this.router.navigate(['/unitBookings/' + this.currentPicoUnitId]); }

  details() {this.router.navigate(['/unitRules/' + this.currentPicoUnitId]); }

}
