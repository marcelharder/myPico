import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
photo_1: string;
  constructor() { }

  ngOnInit() {
    this.photo_1 = "../../assets/images/pico-pictures/DSC_6764.JPG";
  }

}
