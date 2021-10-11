import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/Alertify.service';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {
  submitResult = "";
  username = "";
  password = "";
  model: any = {};

  constructor(
    private authService: AuthService, 
    private alertify: AlertifyService) { }

  ngOnInit() { }

  cancel() { }

  

  login() {
    this.authService.login(this.model).subscribe(data => {
      this.alertify.success('logged in succssfully'); }
    , error => {
      this.alertify.error('Failed to login'); }
    , () => {
     
    });
  }

}
