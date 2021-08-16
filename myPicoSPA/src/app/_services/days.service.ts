import { Injectable } from '@angular/core';
import { ModelMonth } from '../_models/month';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import * as _ from 'underscore';
import { Utilities } from '../_helpers/utilities';
// import 'rxjs/add/operator/takeWhile';

@Injectable()
export class DaysService {
  bu = environment.apiUrl;
  isActive = true;

  constructor(private http: HttpClient) { }

  getDays(year: number, month: number) {
    return this.http.get<Array<string>>(this.bu + 'dates/' + year + '/' + month)
      .map(function (e) {
        // dateNumbers is a Json object
        const arr = [];
        for (const key in e) { if (e.hasOwnProperty(key)) { arr.push(e[key]); } }
        const help = arr[0];
        // remove the zeros from the array
        const util = new Utilities();
        util.findAndReplace(help, 0, null);
        return help[0];
      });
  }


}
