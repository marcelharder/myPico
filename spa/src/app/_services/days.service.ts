import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { dateNumber } from '../_models/dateNumber';

@Injectable({
  providedIn: 'root'
})
export class DaysService {

bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getDays(id: number){ return this.http.get<dateNumber>(this.bu + 'dates/' + id)}

getMonthId(year: number, month: number){
  return this.http.get<number>(this.bu + 'getMonthId/' + month + '/' + year)}

}
