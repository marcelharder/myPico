import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OccupancyService {
  bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getOccupancy(Id: number, year: number, month: number){
  return this.http.get<Array<string>>(this.bu + 'occupancy/' + Id + '/' + year + '/' + month)

}

}
