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

  constructor(public authService: AuthService,
  private alertify: AlertifyService,
  private router: Router
) { }

  ngOnInit() {
     this.authService.currentphotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  loggedIn() { return this.authService.loggedIn(); }

  loggedAdmin() { return this.authService.adminLoggedIn(); }

  getUsername() {
   if (this.authService.decodedToken === undefined) {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.router.navigate(['/home']);
    } else {
   return  this.authService.decodedToken.unique_name; }
  }


  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
