import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { dateNumber } from '../_models/dateNumber';
import { ModelOccupancy } from '../_models/ModelOccypancy';

@Injectable({
  providedIn: 'root'
})
export class DaysService {

bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getDays(unitId: number, month: number, year: number){ return this.http.get<ModelOccupancy>(this.bu + 'dates/' + unitId + '/' + month + '/' + year)}

getMonthId(month: number, year:number){return this.http.get<number>(this.bu + 'getMonthId/' + month + '/' + year)}

}
