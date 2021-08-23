import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/Alertify.service';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  photoUrl: string | undefined;
 
  constructor(public auth: AuthService,
  private alertify: AlertifyService,
  private router: Router
) { }

  ngOnInit() {
     this.auth.currentphotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
   }

  loggedIn() {
    return this.auth.loggedIn(); }

  loggedAdmin() { return this.auth.adminLoggedIn(); }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
