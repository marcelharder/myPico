import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DaysService {

bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getDays(year: number, month: number){
  return this.http.get<Array<string>>(this.bu + 'dates/' + year + '/' + month)
}

}
