import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-general',
  templateUrl: './pico-general.component.html',
  styleUrls: ['./pico-general.component.css']
})
export class PicoGeneralComponent implements OnInit {
photo_1 = "";photo_2 = "";photo_3 = "";
  constructor() { }

  ngOnInit() {
    this.photo_1 = "../../assets/images/pico-pictures/DSC_6764.JPG";
    this.photo_2 = "../../assets/images/pico-pictures/DSC_6748.JPG";
    this.photo_3 = "../../assets/images/pico-pictures/DSC_6786.JPG";
  }

}
