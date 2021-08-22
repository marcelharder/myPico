import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {
  bu: string = environment.apiUrl;

constructor(private http: HttpClient) { }

getPicoUnitId(test: string){return this.http.get<number>(this.bu + 'getUnitId/' + test)}

}
