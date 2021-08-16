import { Component, OnInit } from '@angular/core';
import { Appointment } from '../../_models/appointment';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AppointmentService } from '../../_services/appointment.service';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { ActivatedRoute, Router } from '@angular/router';
import { NgStyle } from '@angular/common';

@Component({
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {

  appointments: Appointment[];
  appt: Appointment;
  pagination: Pagination;
  // appointmentStatus = this.appt.status;
  // paymentStatus = this.appt.paid_InFull;
  isBold = true;
  fontSize = 15;
  isItalic = true;
  isDone = false;
  color = 'white';
  doorNumber = "";
  showDetails = false;

  addStyles() {
    const styles = {
      'font-size.px': this.fontSize,
      'font-style': this.isItalic ? 'italic' : 'normal',
      'font-weight': this.isBold ? 'bold' : 'normal',
      'color': this.color
    };
    return styles;
  }


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService,
    private appts: AppointmentService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.appointments = data['appointments'].result;
      this.pagination = data['appointments'].pagination;
      this.pagination.itemsPerPage = 10;
    });
  }


  loadAppointments() {
    this.appts
      .getAppointmentsForThisUser(
        this.authService.decodedToken.nameid,
        this.pagination.currentPage,
        this.pagination.itemsPerPage
      )
      .subscribe(
        (res: PaginatedResult<Appointment[]>) => {
          this.appointments = res.result;
          this.pagination = res.pagination;
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  accept(a: Appointment) {
    // alert("accept clicked, userid =" + a.userId);
    this.appt = a;
    this.showDetails = true;
  }
  showList(event) {
    this.loadAppointments();
    this.showDetails = false; }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadAppointments();
  }
getStatus(x: string) {
  if (x === "0") { return "Pending"; }
  if (x === "1") { return "Finalized"; }
}
getPayment(x: number) {
  if (x === 0) { return "Not paid"; }
  if (x === 1) { return "Completed"; }
}


}


