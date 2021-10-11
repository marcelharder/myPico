import { Injectable } from '@angular/core';
import { Appointment } from '../_models/appointment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AlertifyService } from './alertify.service';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../_models/pagination';

import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class AppointmentService {
  baseUrl = environment.apiUrl;
  appt: Appointment;
  constructor(private authHttp: HttpClient,
  private alertify: AlertifyService) { }

  getAppointment(userId: number, id: number): Observable<Appointment> { return this.authHttp.get<Appointment>(this.baseUrl + 'GetAppointment/' + userId + '/' + id); }
  getAppointmentsForThisUser(userId: number, page?, itemsPerPage?, messageParams?: string) {
    const paginatedResult: PaginatedResult<Appointment[]> = new PaginatedResult<Appointment[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('UserId', userId.toString()) ;
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage); }
    return this.authHttp
      .get<Appointment[]>(this.baseUrl + 'getAppointmentForUser' , { observe: 'response', params })
      .map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
         }
         return paginatedResult;
      });
  }
  saveAppointment(userId: number, app: Appointment) { return this.authHttp.post<Appointment>(this.baseUrl + 'appt/create/' + userId, app);  }

  updateAppointment(app: Appointment) { return this.authHttp.put<Appointment>(this.baseUrl + 'appt/update' , app); }

  deleteAppointment(userId: number, id: number) { return this.authHttp.delete<Appointment>(this.baseUrl + 'appt/delete/' + userId + '/' + id); }

  finalizeAppointment(userId: number, id: number) {return this.authHttp.post<Appointment>(this.baseUrl + 'appt/finalize/' + userId + '/' + id, {}); }


}
