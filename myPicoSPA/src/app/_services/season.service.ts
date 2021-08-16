import { Injectable } from '@angular/core';
import { Utilities } from '../_helpers/utilities';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SeasonDays } from '../_models/seasonDays';

@Injectable()
export class SeasonService {
  bu = environment.apiUrl;
  constructor(private authHttp: HttpClient) { }

  getSeasonDays(picoUnitId: number, year: number, month: number) {
    return this.authHttp.get<SeasonDays>(this.bu + 'season/' + picoUnitId + '/' + year + '/' + month);
  }

  /* getSeasonPrice(picoUnitId: number, seasonNumber: number) {
    return this.authHttp.get<number>(this.bu + 'seasonPrices/' + picoUnitId + '/' + seasonNumber);
  } */

  sendSeasonDays(userId: number, season: SeasonDays) {
    return this.authHttp.post(this.bu + 'season/' + userId, season).subscribe();
  }

  /* setSeasonPrice(userId: any, picoUnitId: string, seasonNumber: string, newPrice: number) {
    let params = new HttpParams();
    params = params.append("userId", userId);
    params = params.append("picoUnit", picoUnitId);
    params = params.append("seasonNumber", seasonNumber);
    const url = this.bu + 'seasonPrices/' + params;
    return this.authHttp.post(url, newPrice);
  } */
}
