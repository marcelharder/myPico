import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-images',
  templateUrl: './pico-images.component.html',
  styleUrls: ['./pico-images.component.css']
})
export class PicoImagesComponent implements OnInit {

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
  constructor() { }

  ngOnInit() {
      this.photo_1 = "../../assets/images/pico-pictures/DSC_6764.JPG";
      this.photo_2 = "../../assets/images/pico-pictures/DSC_6707.JPG";
      this.photo_3 = "../../assets/images/pico-pictures/DSC_6751.JPG";
      this.photo_4 = "../../assets/images/pico-pictures/DSC_6757.JPG";
      this.photo_5 = "../../assets/images/pico-pictures/DSC_6764.JPG";
      this.photo_6 = "../../assets/images/pico-pictures/DSC_6786.JPG";
      this.photo_7 = "../../assets/images/pico-pictures/DSC_6787.JPG";
      this.photo_8 = "../../assets/images/pico-pictures/DSC_6824.JPG";
      this.photo_9 = "../../assets/images/pico-pictures/DSC_6827.JPG";
      this.photo_10 = "../../assets/images/pico-pictures/DSC_6856.JPG";
  }

}
