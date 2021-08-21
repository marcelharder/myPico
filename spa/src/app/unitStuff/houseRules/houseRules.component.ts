import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-houseRules',
  templateUrl: './houseRules.component.html',
  styleUrls: ['./houseRules.component.css']
})
export class HouseRulesComponent implements OnInit {
  picoUnit = "";
  photo_map = "";
  currency = "PHP";

  constructor() { }

  ngOnInit() {
    this.photo_map = "../../assets/images/unit-pictures/610-A/DSC_6774.JPG";
  }

  availability(){}
  showPHP(){};
  showUSD(){};

}
