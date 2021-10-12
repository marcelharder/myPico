import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AlertifyService } from '../_services/Alertify.service';
import { AuthService } from '../_services/Auth.service';


@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) { }
    canActivate(): boolean {
        if (this.authService.loggedIn()) { return true; }

        this.alertify.error('You are not allowed ...');
        this.router.navigate(['/home']);
        return false;
    }
}
