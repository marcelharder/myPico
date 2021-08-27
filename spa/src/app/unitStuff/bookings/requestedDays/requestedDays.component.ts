import { Component, Input, OnInit } from '@angular/core';
import { requestDays } from 'src/app/_models/requestDays';

@Component({
  selector: 'app-requestedDays',
  templateUrl: './requestedDays.component.html',
  styleUrls: ['./requestedDays.component.css']
})
export class RequestedDaysComponent implements OnInit {
@Input() rd:requestDays | undefined;

constructor() { }

  ngOnInit() {
    
  }

}
