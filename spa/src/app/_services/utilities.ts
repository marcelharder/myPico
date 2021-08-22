import { SeasonDays } from './../_models/seasonDays';
import * as _ from 'underscore';
import { daysModel } from '../_models/daysModel';

export class Utilities {

    constructor() { }

    daysIntoYear(date) {
        return (Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()) - Date.UTC(date.getFullYear(), 0, 0)) / 24 / 60 / 60 / 1000;
    }

    getListDaysArray( year: number, firstMonth: number): Array<daysModel> {
        let index = 0;
        const help = new Array<daysModel>();
        const secondMonth = firstMonth + 1;
        const occupancyStateThisMonth: string[] = [];

       /*  function occupiedDays(y: number, m: number) {
            const occupancy = OccupancyService;
            const ds = DaysService;
            this.occupancyStateThisMonth = [];
            let array_1: string[] = [];
            let array_2: string[] = [];
            this.occupancy.getOccupancy(y, m).subscribe((res) => {array_1 = res; });
            this.ds.getDays(y, m).subscribe((res) => {array_2 = res; });
            for (let i = 0; i < 42; i++) {
                // dit reduceert het aantal dagen weer naar 31 of 30 ipv 42 !!
            if (array_2[i]  !== "0" && array_1[i] === "1") {this.occupancyStateThisMonth.push("99"); } else {this.occupancyStateThisMonth.push("0"); }
            }
        }
 */
        if (firstMonth === 1) { // de maand january en february
            // get the occupancy of year, month en look for 1 which occupied
            for (let i = 0; i < 32; i++) {
              // occupiedDays(year, firstMonth);
              //  help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: +occupancyStateThisMonth[i] });
              help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 });   index++;
                }
            if (this.isLeap(year)) {
                for (let i = 0; i < 30; i++) {
                    // occupiedDays(year, secondMonth);
                    help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 });    index++; }
            } else {
                for (let i = 0; i < 29; i++) {
                    // occupiedDays(year, secondMonth);
                    help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 });     index++; }
            }
        }
        if (firstMonth === 2) { // de maand february en maart
            if (this.isLeap(year)) {
                for (let i = 0; i < 30; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            } else {
                for (let i = 0; i < 29; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            }
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 3) { // de maand maart en april
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 4) { // de maand april en mei
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 5) { // de maand mei en juni
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 6) { // de maand juni en juli
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 7) { // de maand juli en augustus
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 8) { // de maand juli en augustus
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 9) { // de maand juli en augustus
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 10) { // de maand juli en augustus
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }
        if (firstMonth === 11) { // de maand juli en augustus
            for (let i = 0; i < 32; i++) { help.push({ index: index, date: new Date(year, firstMonth - 1, i), value: 0 }); index++; }
            for (let i = 0; i < 31; i++) { help.push({ index: index, date: new Date(year, secondMonth - 1, i), value: 0 }); index++; }
        }








        return help;
    }


    isLeap(year: any) {  return new Date(year, 1, 29).getDate() === 29; }

    getMonths(): Array<string> {
        return [
            'January', 
            'February', 
            'March', 'April', 'May',
            'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    }
    getYears(): Array<string> { return ['2018', '2019', '2020', '2021', '2022']; }

    getSeasonPrice(soort: number): Array<number> {
        const help: Array<number> = [];
        if (soort === 1) { // get the lowseason price
            for (let n = 5000; n <= 6000; n += 10) { help.push(n); }
        }
        if (soort === 2) { // get the lowseason price
            for (let n = 6000; n <= 7000; n += 10) { help.push(n); }
        }
        if (soort === 3) { // get the lowseason price
            for (let n = 7000; n <= 8000; n += 10) { help.push(n); }
        }
        return help;
    }

    translateMonth(n:number) {
        let help = '';
        switch (n) {
            case 0: help = 'January'; break;
            case 1: help = 'February'; break;
            case 2: help = 'March'; break;
            case 3: help = 'April'; break;
            case 4: help = 'May'; break;
            case 5: help = 'June'; break;
            case 6: help = 'July'; break;
            case 7: help = 'August'; break;
            case 8: help = 'September'; break;
            case 9: help = 'October'; break;
            case 10: help = 'November'; break;
            case 11: help = 'December'; break;
        }
        return help;
    }
    getNumberOfMonth(m:string) {
        let help = 0;
        switch (m) {
            case 'January': help = 1; break;
            case 'February': help = 2; break;
            case 'March': help = 3; break;
            case 'April': help = 4; break;
            case 'May': help = 5; break;
            case 'June': help = 6; break;
            case 'July': help = 7; break;
            case 'August': help = 8; break;
            case 'September': help = 9; break;
            case 'October': help = 10; break;
            case 'November': help = 11; break;
            case 'December': help = 12; break;
        }

        return help;
    }

    translateOccupancy(test:string) {
        if (test === '0') { return 'vacant'; } else {
            if (test === '3') { return 'out_of_calendar'; } else { return 'occupied'; }
        }
    }

    cleanUp(input: any) {
        const newArr = [];
        for (let i = 0; i < input.length; i++) {
            if (input[i] != null) { newArr.push(input[i]); }
        }
        return newArr;
    }
    notEmpty<TValue>(value: TValue | null | undefined): value is TValue { return value !== null && value !== undefined; }

    addDays(date: Date, days: number): Date {
        date.setDate(date.getDate() + days);
        return date;
    }

    saveSeason(help: SeasonDays, id: number, value: string): SeasonDays {
        if (id === 1) { help.day_1 = value; }
        if (id === 2) { help.day_2 = value; }
        if (id === 3) { help.day_3 = value; }
        if (id === 4) { help.day_4 = value; }
        if (id === 5) { help.day_5 = value; }
        if (id === 6) { help.day_6 = value; }
        if (id === 7) { help.day_7 = value; }
        if (id === 8) { help.day_8 = value; }
        if (id === 9) { help.day_9 = value; }
        if (id === 10) { help.day_10 = value; }
        return help;
    }

    findAndReplace(help: any, s: any, r: any) {
        const searchVal = s; const replaceVal = r;
        _.each(help, function (obj) {
            _.each(obj, function (value, key) {
                if (value === searchVal) {
                    obj[key] = replaceVal;
                }
            });
        });
        return help;
    }
}

