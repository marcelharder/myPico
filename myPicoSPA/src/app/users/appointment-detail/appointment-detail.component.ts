import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Appointment } from '../../_models/appointment';
import { FormGroup } from '@angular/forms';
import { AppointmentService } from '../../_services/appointment.service';
import { AuthService } from '../../_services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EmailService } from '../../_services/email.service';
import { SmsService } from '../../_services/sms.service';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from '../../_services/user.service';



@Component({
  selector: 'app-appointment-detail',
  templateUrl: './appointment-detail.component.html',
  styleUrls: ['./appointment-detail.component.css']
})
export class AppointmentDetailComponent implements OnInit {
  @Input() appt: Appointment;
  @Output() showList: EventEmitter<boolean> = new EventEmitter();
  picoUnitDescription = "610-A";
  smsSendResult = "";
  mailSendResult = "";
  userName = ""; // de naam van de user die ingelogd is
  usermakingBooking = 0;
  userMakingBookingName = "";


  bookingStatus = "n/a";
  StatusList = [
    new SelectOption('Available', 0),
    new SelectOption('Requested', 5),
    new SelectOption('Occupied', 1)
  ];
  constructor(private appService: AppointmentService,
    private auth: AuthService,
    private r: Router,
    private us: UserService,
    private sms: SmsService,
    private email: EmailService,
    private alertify: AlertifyService) { }
  ngOnInit() {
    if (this.appt.status === "0") { this.bookingStatus = "Pending"; }
    if (this.appt.status === "1") { this.bookingStatus = "Finalized"; }
    this.userName = this.auth.currentUser.username; // de naam van de user die ingelogd is
    this.usermakingBooking = this.appt.userId;
    this.us.getUser(this.appt.userId).subscribe((res) => {
      this.userMakingBookingName = res.knownAs;
    });

  }
  cancel() { this.showList.emit(true); }
  onChange(StatusValue) { console.log(StatusValue); }
  saveDetails() {
    this.appService.updateAppointment(this.appt).subscribe(() => { }, () => { }, () => { this.showList.emit(true); });
  }
  delete() {
    this.appService.deleteAppointment(this.auth.decodedToken.nameid, this.appt.id).subscribe(() => { }, () => { }, () => { this.showList.emit(true); });
  }
  canDelete() {
    const userIsAdmin = true; if (this.auth.currentUser.databaseRole === "admin") { return true; }
  }

  finalize() {
    this.appService.finalizeAppointment(this.auth.currentUser.id, this.appt.id)
      .subscribe(() => { }, (er) => { this.alertify.error("Problem to finalize the booking request"); }, () => {
        const url = '/booking';
        this.r.navigate([url]); } );
  }

  userLinkClicked() {
    const url = '/userDetails/' + this.usermakingBooking;
    const return_url = '';
    // store the return address
    this.auth.setReturnUrl(return_url);
    this.r.navigate([url]);
  }

  canFinalize() {
    const userIsAdmin = true;
    if (this.auth.currentUser.databaseRole === "admin" && this.appt.status !== "1") { return true; }
  }

  printSMSInvoice() { return this.sms.sendInvoiceBySMS(this.appt.id).subscribe((e) => { this.mailSendResult = ""; this.smsSendResult = e.toString(); }); }

  printMailInvoice() { return this.email.sendInvoiceByMail(this.appt.id).subscribe((e) => { this.smsSendResult = ""; this.mailSendResult = e.toString(); }); }

}
export class SelectOption {
  constructor(public St: string, public Val: number) { }
}
