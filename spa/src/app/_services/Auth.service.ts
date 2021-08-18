import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User | undefined;

/*   Hospital = new BehaviorSubject<string>('0');
  currentHospital = this.Hospital.asObservable();
 */
 

  photoUrl = new BehaviorSubject<string>('0');
  currentphotoUrl = this.photoUrl.asObservable();

  /* soortProcedure = new BehaviorSubject<string>('0');
  currentSoortProcedure = this.soortProcedure.asObservable();

  dst = new BehaviorSubject<string>('0');
  currentDst = this.dst.asObservable(); */

  constructor(private http: HttpClient) {}


  //changeCurrentHospital(sh: string) { this.Hospital.next(sh); }
  changeCurrentPhotoUrl(sh: string) { this.photoUrl.next(sh); }
  
  //changeSoortOperatie(sh: string) { this.soortProcedure.next(sh); }
  //changeDst(sh: string) { this.dst.next(sh); }
  //changeLtk(sh: boolean) { this.ltk.next(sh); }


  login(model: any) {
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            console.log(this.decodedToken);
        }
      })
    );
  }

register(model: any) {return this.http.post(this.baseUrl + 'auth/register', model); }

loggedIn() {
  const token = this.jwtHelper.tokenGetter();
  if (!token) { return false; }
  return !this.jwtHelper.isTokenExpired(token);
}

adminLoggedIn() {
  if (this.loggedIn()) {
      if(this.decodedToken.role === "admin"){return true}
  } 
  return false;
}

updatePassword(model: any) { return this.http.put<boolean>(this.baseUrl + 'auth/changePassword', model); }



}
