import { Component, OnInit } from '@angular/core';
import { GeneralService } from '../_services/general.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private gen: GeneralService) { }

  ngOnInit() {
    this.gen.changeChosen(false);
  }

}
