import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GeneralService } from 'src/app/_services/general.service';
import { AuthService } from '../../_services/Auth.service';

@Component({
  selector: 'app-unitPictures',
  templateUrl: './unitPictures.component.html',
  styleUrls: ['./unitPictures.component.css']
})
export class UnitPicturesComponent implements OnInit {
  currentPicoUnitId = 0;;
  pictureLocation = "";

  unitPictures: Array<string> = [];
  photo_0: string = "";
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

  constructor(private route:ActivatedRoute, private gen: GeneralService,
    private auth:AuthService, 
    private router:Router) { }

  ngOnInit() {
    this.currentPicoUnitId = +this.route.snapshot.params.id;

    this.gen.getPicoUnitDetails(this.currentPicoUnitId).subscribe((next)=>{this.pictureLocation = next.picoUnitNumber})

    // get the pictures from the backend
    this.gen.getPicoUnitPictures(this.currentPicoUnitId).subscribe((next)=>{
      
      this.unitPictures = next;
      
      this.photo_1 = this.unitPictures[0];
      this.photo_2 = this.unitPictures[1];
      this.photo_3 = this.unitPictures[2];
      this.photo_4 = this.unitPictures[3];
      this.photo_5 = this.unitPictures[4];
      this.photo_6 = this.unitPictures[5];
      this.photo_7 = this.unitPictures[6];
      this.photo_8 = this.unitPictures[7];
      this.photo_9 = this.unitPictures[8];
      this.photo_10 = this.unitPictures[9]; 
    
    })
    
       
    
   

  }
  

  

  details() {this.router.navigate(['/unitRules/' + this.currentPicoUnitId]); }

}
