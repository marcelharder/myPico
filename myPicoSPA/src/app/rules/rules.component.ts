import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.css']
})
export class RulesComponent implements OnInit {
photo_2: string;
  constructor() { }

  ngOnInit() {
    this.photo_2 = "../../assets/images/pico-pictures/DSC_6707.JPG";
  }

}
