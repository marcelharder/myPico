import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
whereToReturnTo: string;
model: any = {};

constructor(
  private authService: AuthService,
  private alertify: AlertifyService,
  private router: Router) { }

  ngOnInit() {  }



  login() {
    this.authService.login(this.model).subscribe(data => {
      this.alertify.success('logged in succssfully'); }
    , error => {
      this.alertify.error('Failed to login'); }
    , () => {
      // figure out where to go to
         this.authService.currentReturnUrl.subscribe(r => this.whereToReturnTo = r);
         this.router.navigate([this.whereToReturnTo]);
      //this.router.navigate(['/home']);
    });
  }

  

  loggedIn() {return this.authService.loggedIn(); }

  register() {this.router.navigate(['/register']); }

}
