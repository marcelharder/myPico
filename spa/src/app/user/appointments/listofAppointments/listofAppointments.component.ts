import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/Auth.service';

@Component({
  selector: 'app-listofAppointments',
  templateUrl: './listofAppointments.component.html',
  styleUrls: ['./listofAppointments.component.css']
})
export class ListofAppointmentsComponent implements OnInit {

  constructor(private auth: AuthService) { }

  ngOnInit() {
    
  }

}
