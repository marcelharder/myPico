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
import { UnitPicturesComponent } from './unitPictures/unitPictures.component';
import { ProfileComponent } from './user/Profile/Profile.component';

const routes: Routes = [{ path: '', component: HomeComponent },
{ path: 'contact', component: ContactComponent },
{ path: 'about', component: AboutComponent },
{ path: 'home', component: HomeComponent },
{ path: 'picoMap', component: PicoMapComponent },
{ path: 'picoGeneral', component: PicoGeneralComponent },
{ path: 'picoPictures', component: PicoImagesComponent },
{ path: 'picoRules', component: PicoRulesComponent },
{ path: 'unitPictures/:id', component: UnitPicturesComponent },
{ path: 'login', component: LoginComponent },
{ path: 'profile', component: ProfileComponent },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
