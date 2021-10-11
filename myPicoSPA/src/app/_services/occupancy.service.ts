import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Utilities } from '../_helpers/utilities';

@Injectable()
export class OccupancyService {
  bu = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getPicoUnitNumberFromId(Id: number) {return "610-A"; }

  getOccupancy(picoUnitId: number, year: number, month: number)  {
  return this.http.get<Array<string>>(this.bu + 'occupancy/' + this.getPicoUnitNumberFromId(picoUnitId) + '/' + year + '/' + month )
  .map(function(e) {
    const arr = [];
    for (const key in e) { if (e.hasOwnProperty(key)) {arr.push(e[key]); } }
    const help = arr[0];
    // get the correct class
    const util = new Utilities();
    util.findAndReplace(help, 0, 'vacant');
    util.findAndReplace(help, 1, 'occupied');
    util.findAndReplace(help, 3, 'out_of_calendar');

    console.log(help[0]);
    return help[0];
    } );
  }
}
