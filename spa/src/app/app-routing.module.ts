import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './About/About.component';
import { ContactComponent } from './Contact/Contact.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Login/Login.component';
import { PicoGeneralComponent } from './picodeloroStuff/pico-general/pico-general.component';
import { PicoImagesComponent } from './picodeloroStuff/pico-images/pico-images.component';
import { PicoRulesComponent } from './picodeloroStuff/pico-rules/pico-rules.component';
import { PicoMapComponent } from './picodeloroStuff/picoMap/picoMap.component';
import { BookingsComponent } from './unitStuff/bookings/bookings.component';
import { HouseRulesComponent } from './unitStuff/houseRules/houseRules.component';
import { RatingsComponent } from './unitStuff/ratings/ratings.component';
import { UnitPicturesComponent } from './unitStuff/unitPictures/unitPictures.component';
import { ListofAppointmentsComponent } from './user/appointments/listofAppointments/listofAppointments.component';
import { ProfileComponent } from './user/Profile/Profile.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
{ path: '', component: HomeComponent },
{ path: 'contact', component: ContactComponent },
{ path: 'about', component: AboutComponent },
{ path: 'home', component: HomeComponent },
{ path: 'picoMap', component: PicoMapComponent },
{ path: 'picoGeneral', component: PicoGeneralComponent },
{ path: 'picoPictures', component: PicoImagesComponent },
{ path: 'picoRules', component: PicoRulesComponent },
{ path: 'unitRules/:id', component: HouseRulesComponent },
{ path: 'unitRatings/:id', component: RatingsComponent },
{ path: 'unitBookings/:id', component: BookingsComponent },
{ path: 'unitPictures/:id', component: UnitPicturesComponent },
{ path: 'login', component: LoginComponent },

{
  path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children: [

    { path: 'listOfAppointments', component: ListofAppointmentsComponent}, 
    { path: 'profile', component: ProfileComponent },
     
    
  ]
},



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
