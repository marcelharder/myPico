import { PicoUnit } from './../_models/PicoUnit';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class PicoUnitService {

  baseUrl = environment.apiUrl;
  constructor(private authHttp: HttpClient) { }

  getPicoUnitManagedByThisUser(userId: number) {
    return this.authHttp.get<PicoUnit>(this.baseUrl + 'unitManaged/' + userId);
  }

  getPicoUnitFromId(userId: number, id: number) {
    return this.authHttp.get<PicoUnit>(this.baseUrl + 'unitDetails/' + userId + '/' + id);
  }

  saveSeasonPrices(userId: number, pic: PicoUnit) {
    return this.authHttp.post(this.baseUrl + 'unitDetails/' + userId, pic).subscribe();
  }

}
