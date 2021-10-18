import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CarouselModule} from 'ngx-bootstrap/carousel';
import { AccordionModule} from 'ngx-bootstrap/accordion';
import { BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import {PaginationModule} from 'ngx-bootstrap/pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './Contact/Contact.component';
import { AboutComponent } from './About/About.component';
import { LoginComponent } from './Login/Login.component';
import { HttpClientModule } from '@angular/common/http';
import { AlertifyService } from './_services/Alertify.service';
import { FormsModule } from '@angular/forms';
import { UnitPicturesComponent } from './unitStuff/unitPictures/unitPictures.component';
import { PicoGeneralComponent } from './picodeloroStuff/pico-general/pico-general.component';
import { PicoMapComponent } from './picodeloroStuff/picoMap/picoMap.component';
import { PicoImagesComponent } from './picodeloroStuff/pico-images/pico-images.component';
import { PicoRulesComponent } from './picodeloroStuff/pico-rules/pico-rules.component';
import { HouseRulesComponent } from './unitStuff/houseRules/houseRules.component';
import { RatingsComponent } from './unitStuff/ratings/ratings.component';
import { BookingsComponent } from './unitStuff/bookings/bookings.component';
import { FirstMonthComponent } from './unitStuff/bookings/first-month/first-month.component';
import { SecondMonthComponent } from './unitStuff/bookings/second-month/second-month.component';
import { MonthSummaryComponent } from './unitStuff/bookings/first-month-summary/first-month-summary.component';
import { SecondMonthSummaryComponent } from './unitStuff/bookings/second-month-summary/second-month-summary.component';
import { ListofAppointmentsComponent } from './user/appointments/listofAppointments/listofAppointments.component';
import { MessageComponent } from './Message/Message.component';
import { GeneralService } from './_services/general.service';
import { OccupancyService } from './_services/occupancy.service';
import { AuthService } from './_services/Auth.service';
import { TimeagoModule } from 'ngx-timeago';

@NgModule({
  declarations: [	
     					
      AppComponent,
      NavBarComponent,
      HomeComponent,
      ContactComponent,
      AboutComponent,
      LoginComponent,
      UnitPicturesComponent,
      PicoGeneralComponent,
      PicoMapComponent,
      PicoImagesComponent,
      PicoRulesComponent,
      HouseRulesComponent,
      RatingsComponent,
      BookingsComponent,
      FirstMonthComponent,
      SecondMonthComponent,
      MonthSummaryComponent,
      SecondMonthSummaryComponent,
      ListofAppointmentsComponent,
      MessageComponent
   ],
  imports: [
    TimeagoModule.forRoot(),
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    CarouselModule.forRoot(),
    AccordionModule.forRoot(),
    PaginationModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    AlertifyService,
    GeneralService,
    OccupancyService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
