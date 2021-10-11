import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { RequestedMonth } from '../_models/RequestedMonth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any | undefined;
  currentUser: User | undefined;

  firstMonth = new BehaviorSubject<RequestedMonth>( { appointmentId: 0, picoUnit: 0, year: 0, month: 0 });
  fmo = this.firstMonth.asObservable();
  secondMonth = new BehaviorSubject<RequestedMonth>( { appointmentId: 0, picoUnit: 0, year: 0, month: 0 });
  smo = this.secondMonth.asObservable();
  picoUnit = new BehaviorSubject<string>('0');
  currentPicoUnit = this.picoUnit.asObservable();
 

  constructor(private http: HttpClient, private router:Router) {}

  
  changePicoUnitId(sh: string){this.picoUnit.next(sh);}
  setFirstMonth(sh: RequestedMonth){this.firstMonth.next(sh);}
  setSecondMonth(sh: RequestedMonth){this.secondMonth.next(sh);}

  
  
  login(model: any) {
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) => {
        this.currentUser = response.user;
        if (this.currentUser) {
          localStorage.setItem('token', response.tokenString);
          this.decodedToken = this.jwtHelper.decodeToken(response.tokenString);
          console.log(this.decodedToken);
          this.router.navigate(['/home']);
        }
      })
    );
  }

register(model: any) {return this.http.post(this.baseUrl + 'auth/register', model); }

loggedIn() {return !this.jwtHelper.isTokenExpired(localStorage.getItem('token')!);}

logOut(){
  localStorage.setItem('chosen', '0');
  localStorage.removeItem('token');
  localStorage.removeItem('user');
}

adminLoggedIn() {
  if (this.loggedIn()) {
      if(this.decodedToken.role === "admin"){return true}
  } 
  return false;
}

updatePassword(model: any) { return this.http.put<boolean>(this.baseUrl + 'auth/changePassword', model); }



}

