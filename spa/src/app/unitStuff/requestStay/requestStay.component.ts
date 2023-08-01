
import { Component, Input, OnInit } from '@angular/core';
import { Appointment } from 'src/app/_models/appointment';
import { Message } from 'src/app/_models/Message';
import { PicoUnit } from 'src/app/_models/PicoUnit';
import { AuthService } from 'src/app/_services/Auth.service';
import { GeneralService } from 'src/app/_services/general.service';

@Component({
  selector: 'app-requestStay',
  templateUrl: './requestStay.component.html',
  styleUrls: ['./requestStay.component.css']
})
export class RequestStayComponent implements OnInit {
  @Input() message: Message;
  @Input() appointment: Appointment;

  caretakerName = "";
  currentUnit = "";
  senderName = "";
  senderEmail = "";

  constructor(private gen: GeneralService, private auth: AuthService) { }

  ngOnInit() {
    this.gen.getPicoUnitDetails(this.appointment.picoUnitId).subscribe((next)=>{
      this.caretakerName = next.Caretaker;
      this.currentUnit = next.picoUnitNumber;
    });
  }

  chat(){if(this.auth.loggedIn()){return true;} else {return false;}}

}
