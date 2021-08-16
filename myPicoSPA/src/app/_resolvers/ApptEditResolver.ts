import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AppointmentService } from '../_services/appointment.service';
import { Appointment } from '../_models/appointment';


@Injectable()
export class ApptEditResolver  implements Resolve<Appointment> {
    constructor(
        private apptService: AppointmentService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) {}
        resolve(route: ActivatedRouteSnapshot): Observable<Appointment> {
            const id = route.queryParams.get('id');
            return this.apptService.getAppointment(this.authService.decodedToken.nameid, id).catch(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return Observable.of(null);
            });
        }
}


