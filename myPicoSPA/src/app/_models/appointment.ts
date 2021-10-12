export interface Appointment {
  picoUnitId: number;
  picoUnitPhotoUrl: string;
  userId: number;
  requestedDays: Array<string>;
  startDate: Date;
  endDate: Date;
  noOfNights: number;
  id: number;
  year: number;
  month: number;
  day: number;
  status: string;
  rent: number;
  downPayment: number;
  paid_InFull: number;
  comment: string;
}
