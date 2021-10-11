import { PicoUnitService } from './_services/pico-unit.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';
import { BsDropdownModule, CarouselModule, BsDatepickerModule , TabsModule, PaginationModule, AccordionModule} from 'ngx-bootstrap';



import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { appRoutes } from './_helpers/route';
import { FileUploadModule } from 'ng2-file-upload';
import { HomeComponent } from './home/home.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { TodoComponent } from './todo/todo.component';
import { PicturesComponent } from './pictures/pictures.component';
import { AlertifyService } from './_services/alertify.service';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { FirstMonthComponent } from './schedule/first-month/first-month.component';
import { SecondMonthComponent } from './schedule/second-month/second-month.component';
import { OccupancyService } from './_services/occupancy.service';
import { DaysService } from './_services/days.service';
import { PhotoEditorComponent } from './users/photoEditor/PhotoEditor.component';
import { UserService } from './_services/user.service';
import { UserEditResolver } from './_resolvers/UserEditResolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AppointmentComponent } from './users/appointments/appointment.component';
import { TimeAgoPipe } from 'time-ago-pipe';
import { UnitDetailsComponent } from './unit/unit-details/unit-details.component';
import { AppointmentDetailComponent } from './users/appointment-detail/appointment-detail.component';
import { AppointmentService } from './_services/appointment.service';
import { ApptListResolver } from './_resolvers/ApptListResolver';
import { AccordionComponent } from './accordion/accordion.component';
import { RulesComponent } from './rules/rules.component';
import { AccessibilityComponent } from './accessibility/accessibility.component';
import { PicoGeneralComponent } from './pico/pico-general/pico-general.component';
import { PicoMapsComponent } from './pico/pico-maps/pico-maps.component';
import { PicoRulesComponent } from './pico/pico-rules/pico-rules.component';
import { PicoImagesComponent } from './pico/pico-images/pico-images.component';
import { AgmCoreModule } from '@agm/core';
import { EmailService } from './_services/email.service';
import { SmsService } from './_services/sms.service';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/UserDetailResolver';
import { ConfigComponent } from './config/config.component';
import { SeasonService } from './_services/season.service';



export function getAccessToken(): string {
  return localStorage.getItem('token');
}

export const jwtConfig = {
  tokenGetter: getAccessToken,
  whitelistedDomains: ['localhost:5000']
};

export function googleApiKey(): string {
  return 'AIzaSyAOt32SxX3bmtxrfGtpi8DuW4gEqApUVgs';
}



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    ScheduleComponent,
    TodoComponent,
    PicturesComponent,
    LoginComponent,
    RegisterComponent,
    UserEditComponent,
    FirstMonthComponent,
    SecondMonthComponent,
    PhotoEditorComponent,
    TimeAgoPipe,
    AppointmentComponent,
    UnitDetailsComponent,
    AppointmentDetailComponent,
    AccordionComponent,
    RulesComponent,
    AccessibilityComponent,
    PicoGeneralComponent,
    PicoMapsComponent,
    PicoImagesComponent,
    PicoRulesComponent,
    UserDetailComponent,
    ConfigComponent
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    FileUploadModule,
    RouterModule.forRoot(appRoutes),
    CarouselModule.forRoot(),
    JwtModule.forRoot({config: jwtConfig}),
    AccordionModule.forRoot(),
    AgmCoreModule.forRoot({apiKey: googleApiKey()})

  ],
  providers: [AlertifyService, SeasonService,
    EmailService,
    SmsService,
    PicoUnitService,
    AuthService,
    OccupancyService,
    DaysService,
    UserService,
    PreventUnsavedChanges,
    UserEditResolver,
    AppointmentService,
    UserDetailResolver,
    ApptListResolver],

  bootstrap: [AppComponent]
})
export class AppModule { }
