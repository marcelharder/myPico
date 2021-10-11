import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pico-maps',
  templateUrl: './pico-maps.component.html',
  styleUrls: ['./pico-maps.component.css']
})
export class PicoMapsComponent implements OnInit {
  lat = 14.190707;
  lng = 120.597489;

  constructor() { }

  ngOnInit() {
  }

}
