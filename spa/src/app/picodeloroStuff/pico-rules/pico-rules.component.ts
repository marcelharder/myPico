import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-rules',
  templateUrl: './pico-rules.component.html',
  styleUrls: ['./pico-rules.component.css']
})
export class PicoRulesComponent implements OnInit {
photo_1 = "";
currency = "PHP";
  constructor() { }

  ngOnInit() {   this.photo_1 = "../../assets/images/pico-pictures/DSC_6764.JPG";  }

  showUSD() {this.currency = 'USD'; }
  showPHP() {this.currency = 'PHP'; }

}
