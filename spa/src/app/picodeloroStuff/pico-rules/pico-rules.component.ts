import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-rules',
  templateUrl: './pico-rules.component.html',
  styleUrls: ['./pico-rules.component.css']
})
export class PicoRulesComponent implements OnInit {
photo_1 = "";photo_2 = "";photo_3 = "";
currency = "PHP";
  constructor() { }

  ngOnInit() { 
      this.photo_1 = "../../assets/images/pico-pictures/DSC_7160.JPG";
      this.photo_2 = "../../assets/images/pico-pictures/DSC_6898.JPG";
      this.photo_3 = "../../assets/images/pico-pictures/DSC_6907.JPG";
    
    
    }

  showUSD() {this.currency = 'USD'; }
  showPHP() {this.currency = 'PHP'; }

}
