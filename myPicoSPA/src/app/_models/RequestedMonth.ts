export class RequestedMonth {
    month?: number;
    year?: number;
    picoUnit?: number;

    constructor(pu: number, m: number, y: number) {
        this.month = m;
        this.year = y;
        this.picoUnit = pu;
    }
}
