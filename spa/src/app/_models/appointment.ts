import { requestDays } from "./requestDays";

export interface Appointment {
  picoUnitId: number;
  picoUnitPhotoUrl: string;
  userId: number;
  requestedDays: Array<requestDays>;
  startDate: Date;
  endDate: Date;
  noOfNights: number;
  id: number;
  year: number;
  month: number;
  day: number;
  status: string;
  rent: number;
  rentUSD: number;
  downPayment: number;
  paid_InFull: number;
  comment: string;
 
}
