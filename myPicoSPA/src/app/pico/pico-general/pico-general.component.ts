import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-general',
  templateUrl: './pico-general.component.html',
  styleUrls: ['./pico-general.component.css']
})
export class PicoGeneralComponent implements OnInit {
photo_1 = "";
  constructor() { }

  ngOnInit() {
    this.photo_1 = "../../assets/images/pico-pictures/DSC_6764.JPG";
  }

}
