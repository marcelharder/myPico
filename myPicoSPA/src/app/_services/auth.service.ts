import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ExtendedUser } from '../_models/AU';

@Injectable()
export class AuthService {
    bu = environment.apiUrl;
    // baseUrl = 'http://localhost:5000/api/auth/';
    userToken: any;
    decodedToken: any;
    currentUser: User;

    private photoUrl = new BehaviorSubject<string>('../../assets/user.png');
    private returnUrl = new BehaviorSubject<string>('/schedule');

    currentPhotoUrl = this.photoUrl.asObservable();
    currentReturnUrl = this.returnUrl.asObservable();

    constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

    changeMemberPhoto(photoUrl: string) { this.photoUrl.next(photoUrl); }
    setReturnUrl(rurl: string) { this.returnUrl.next(rurl); }


    login(model: any) {
        return this.http.post<ExtendedUser>(this.bu + 'auth/login', model, {
            headers: new HttpHeaders()
                .set('Content-Type', 'application/json')
        })
            .map(user => {
                if (user && user.tokenString) {
                    localStorage.setItem('token', user.tokenString);
                    localStorage.setItem('user', JSON.stringify(user.user));
                    this.decodedToken = this.jwtHelperService.decodeToken(user.tokenString);
                    this.currentUser = user.user;
                    this.userToken = user.tokenString;
                    if (this.currentUser.photoUrl !== null) {
                        this.changeMemberPhoto(this.currentUser.photoUrl);
                    } else {
                        this.changeMemberPhoto('../../assets/user.png');
                    }
                }
            });
    }
    register(user: User) {
        return this.http.post(this.bu + 'auth/register', user, {
            headers: new HttpHeaders()
                .set('Content-Type', 'application/json')
        });

    }
    loggedIn() {
        const token = this.jwtHelperService.tokenGetter();
        if (!token) { return false; }
        return !this.jwtHelperService.isTokenExpired(token);
    }

    adminLoggedIn() {
        if (this.loggedIn()) {
            this.currentUser = JSON.parse(localStorage.getItem('user'));
            if (this.currentUser.databaseRole === 'admin') { return true; }
        } 
        return false;
    }

}

