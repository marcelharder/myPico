import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  user: User;
  @ViewChild('editForm') editForm: NgForm;
  photoUrl: string;


  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private alertify: AlertifyService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {   this.user = data['user'];   });
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.success('Profile updated successfully');
      this.editForm.reset(this.user);
      }, error => {this.alertify.error(error); });
  }

  changeMainPhoto(photoUrl) { this.user.photoUrl = photoUrl; }
}
