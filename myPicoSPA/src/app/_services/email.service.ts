import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class EmailService {
  baseUrl = environment.apiUrl;
  constructor(private auth: AuthService, private authHttp: HttpClient) { }


  sendInvoiceByMail(appointmentId: number) {
    const currentUserId = this.auth.decodedToken.nameid;
    return this.authHttp.post(this.baseUrl + 'appt/sendMail/' + currentUserId + "/" + appointmentId, {});
  }
  
}
