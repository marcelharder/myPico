import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-unit-details',
  templateUrl: './unit-details.component.html',
  styleUrls: ['./unit-details.component.css']
})
export class UnitDetailsComponent implements OnInit {
picoUnit = "";
photo_map = "";
currency = "PHP";
  constructor(private route: ActivatedRoute, private router: Router, private auth: AuthService) { }

  ngOnInit() {
    this.picoUnit = this.route.snapshot.paramMap.get('id');
    this.photo_map = "../../assets/images/pico-pictures/map-pico.jpg";
  }
  availability() {
    if (this.auth.loggedIn()) {
     this.router.navigate(['/schedule/' + this.picoUnit]);
    } else {this.router.navigate(['/login']); }
    }
    showUSD() {this.currency = 'USD'; }
    showPHP() {this.currency = 'PHP'; }

}
