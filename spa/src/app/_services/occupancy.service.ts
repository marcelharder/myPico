import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { dateOccupancy } from '../_models/dateOccupancy';

@Injectable({
  providedIn: 'root'
})
export class OccupancyService {
  bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getOccupancy(UnitId: number, id: number){
  return this.http.get<dateOccupancy>(this.bu + 'occupancy/' + UnitId + '/' + id)

}

}
