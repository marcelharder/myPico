import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  photoUrl: string;

  constructor(public authService: AuthService,
  private alertify: AlertifyService,
  private router: Router
) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  loggedIn() { return this.authService.loggedIn(); }

  loggedAdmin() { return this.authService.adminLoggedIn(); }

  getUsername() {
   if (this.authService.decodedToken === undefined) {
    this.authService.userToken = null;
    this.authService.currentUser = null;
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.router.navigate(['/home']);
    } else {
   return  this.authService.decodedToken.unique_name; }
  }


  logout() {
    this.authService.userToken = null;
    this.authService.currentUser = null;
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }


}
