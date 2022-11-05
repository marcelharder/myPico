import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DropItem } from '../_models/dropItem';
import { AlertifyService } from '../_services/Alertify.service';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  username = "";
  password = "";
  registerModel: any = {username:"", password:"", city:"", country:"", gender:"", knownAs:""};
  genderoptions:Array<DropItem> = [];
  countryoptions:Array<DropItem> = [];

  constructor(
    private router:Router, 
    private alertify:AlertifyService, 
    private auth: AuthService) { }

  ngOnInit() {
    this.genderoptions.push({value:"1", description:"male"});
    this.genderoptions.push({value:"2", description:"female"});

    this.countryoptions.push({value:"Philippines", description:"Philippines"});
    this.countryoptions.push({value:"China", description:"China"});
    this.countryoptions.push({value:"US", description:"US"});
    this.countryoptions.push({value:"Netherlands", description:"Netherlands"});
  }

  cancel(){this.alertify.success("Cancelling")}

  register(){
    this.registerModel.knownAs = this.registerModel.username;
    if(this.registerModel.username !== undefined && this.registerModel.password !== undefined){
    this.auth.register(this.registerModel).subscribe(()=>{})
    this.alertify.success("Registering")}
  }

}
