import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ModelOccupancy } from '../_models/ModelOccypancy';
import { SeasonDays } from '../_models/seasonDays';

@Injectable({
  providedIn: 'root'
})
export class OccupancyService {
  bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getOccupancy(UnitId: number, month: number, year: number){return this.http.get<ModelOccupancy>(this.bu + 'dates/' + UnitId + '/' + month + '/' + year)}

saveOccupancy(doc: SeasonDays){return this.http.put<number>(this.bu + 'occupancy/update/', doc)}

deleteOccupancy(id: number){return this.http.delete<number>(this.bu + 'occupancy/delete/' + id)}



}
