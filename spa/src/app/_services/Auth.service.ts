import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';

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

  picoUnit = new BehaviorSubject<string>('1');
  currentPicoUnit = this.picoUnit.asObservable();



  /* soortProcedure = new BehaviorSubject<string>('0');
  currentSoortProcedure = this.soortProcedure.asObservable();

  dst = new BehaviorSubject<string>('0');
  currentDst = this.dst.asObservable(); */

  constructor(private http: HttpClient, private router:Router) {}


  //changeCurrentHospital(sh: string) { this.Hospital.next(sh); }
  changeCurrentPhotoUrl(sh: string) { this.photoUrl.next(sh); }
  changeCurrentPicoUnitId(sh: string){this.picoUnit.next(sh);}

  
  //changeSoortOperatie(sh: string) { this.soortProcedure.next(sh); }
  //changeDst(sh: string) { this.dst.next(sh); }
  //changeLtk(sh: boolean) { this.ltk.next(sh); }


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

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token!);
}

adminLoggedIn() {
  if (this.loggedIn()) {
      if(this.decodedToken.role === "admin"){return true}
  } 
  return false;
}

updatePassword(model: any) { return this.http.put<boolean>(this.baseUrl + 'auth/changePassword', model); }



}

