import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-ratings',
  templateUrl: './ratings.component.html',
  styleUrls: ['./ratings.component.css']
})
export class RatingsComponent implements OnInit {
  currentPicoUnitId = 0;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.currentPicoUnitId = +this.route.snapshot.params.id;
  }

}
