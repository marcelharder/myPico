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

  picoUnitId = 0;

  constructor(public auth: AuthService,
    private gen: GeneralService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  loggedIn() { return this.auth.loggedIn(); }

  loggedAdmin() { return this.auth.adminLoggedIn(); }



  unitChosen(): boolean {
  if (localStorage.getItem('chosen') === '1') return true;
  else return false;
  } 

  selectUnit(picoUnitNumber: string) {
    this.picoUnitId = +picoUnitNumber;
    this.gen.changeUnitName(this.picoUnitId);
    this.gen.changeChosen(true);
    // go straight to the details page
    this.showDetails("./unitRules/");
    
  }

  showDetails(url: string){
    this.router.navigate([url + this.picoUnitId]);
  }



  logout() {
    this.auth.logOut();
    this.router.navigate(['/home']);
    this.alertify.message('logged out');
}
  }


