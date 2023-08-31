import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/Alertify.service';
import { AuthService } from '../_services/Auth.service';
import { GeneralService } from '../_services/general.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  photoUrl: string | undefined;

  owner = 0;

  picoUnitId = 0;

  constructor(public auth: AuthService,
    private gen: GeneralService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
    this.auth.currentRole.subscribe(next => {
      if (next === "owner") { this.owner = 1; }
    })
  }

  loggedIn() { return this.auth.loggedIn(); }

  loggedAdmin() { return this.auth.adminLoggedIn(); }



  unitChosen(): boolean {
    if (localStorage.getItem('chosen') === '1') return true;
    else return false;
  }

  selectAnotherUnit(){localStorage.setItem("chosen","0");}

  selectUnit(name: string) {
    this.gen.getPicoUnitId(name).subscribe((next) => {
      this.picoUnitId = next;
      this.auth.changePicoUnitId(next.toString());
      this.gen.changeChosen(true);
    });
    // go straight to the details page
    this.showDetails("./unitRules/");

  }

  showDetails(url: string) {
    this.router.navigate([url + this.picoUnitId]);
  }



  logout() {
    this.auth.logOut();
    this.router.navigate(['/home']);
    this.alertify.message('logged out');
  }
}


