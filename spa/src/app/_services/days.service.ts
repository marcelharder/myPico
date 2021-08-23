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

getDays(year: number, month: number){
  return this.http.get<dateNumber>(this.bu + 'dates/' + year + '/' + month)
}

}
