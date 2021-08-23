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

getMonthFromNo(no: number): String{
  let test:Array<String> = [];
  test.push("January"); test.push("February");  test.push("March");  test.push("April");
  test.push("May");  test.push("June");  test.push("July");  test.push("August");
  test.push("September");  test.push("October");  test.push("November");  test.push("December");
  return test[no];
}

}
