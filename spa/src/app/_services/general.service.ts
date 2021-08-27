import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {
bu: string = environment.apiUrl;

picoName = new BehaviorSubject<string>('0');
currentPicoName = this.picoName.asObservable();
  
constructor(private http: HttpClient) { }

getPicoUnitId(test: string){return this.http.get<number>(this.bu + 'getUnitId/' + test)}
getPicoUnitName(test: number){return this.http.get<string>(this.bu + 'getUnitName/' + test)}

getMonthFromNo(no: number): String{
  let test:Array<String> = [];
  test.push("");
  test.push("January"); test.push("February");  test.push("March");  test.push("April");
  test.push("May");  test.push("June");  test.push("July");  test.push("August");
  test.push("September");  test.push("October");  test.push("November");  test.push("December");
  return test[no];
}

changeUnitName(no: number){
  let help = "";
  this.getPicoUnitName(no).subscribe((data)=>{help = data; });
  debugger;
  this.picoName.next(help);
}

changeChosen(sh: boolean){
  if(sh){localStorage.setItem("chosen", '1');} else {localStorage.setItem("chosen", '0')};
}

}
