import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AuthService } from '../../_services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: User;
  whereToReturnTo: string;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService,
    private us: UserService) { }

  ngOnInit() {
    this.route.data.subscribe(
      (data) => {
        console.log(data);
        this.user = data['user'];
      },
      (error) => {
        console.log(error);
      });
  }

  doneClicked() {
  this.router.navigate(['/booking']);
  }

}
