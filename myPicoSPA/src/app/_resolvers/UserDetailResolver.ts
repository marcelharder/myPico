import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AppointmentService } from '../_services/appointment.service';
import { User } from '../_models/user';



@Injectable()
export class UserDetailResolver  implements Resolve<User> {
    constructor(
        private userService: UserService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) {}
        resolve(route: ActivatedRouteSnapshot): Observable<User> {
            const id: number = route.params.id;
            return this.userService.getUser(id).catch(error => {  this.alertify.error('Problem retrieving data'); this.router.navigate(['/home']);return Observable.of(null);
            });
        }
}
