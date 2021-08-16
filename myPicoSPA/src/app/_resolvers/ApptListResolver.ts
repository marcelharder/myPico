import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { Appointment } from "../_models/appointment";
import { AppointmentService } from "../_services/appointment.service";
import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ApptListResolver implements Resolve<Appointment[]> {
    pageSize = 5;
    pageNumber = 1;

    constructor(
        private appt: AppointmentService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Appointment[]> {
        return this.appt.getAppointmentsForThisUser(this.authService.decodedToken.nameid,
            this.pageNumber,
            this.pageSize).catch(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return Observable.of(null);
            });
    }
}



