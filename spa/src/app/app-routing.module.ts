import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './About/About.component';
import { ContactComponent } from './Contact/Contact.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Login/Login.component';

const routes: Routes = [{ path: '', component: HomeComponent },
{ path: 'contact', component: ContactComponent },
{ path: 'about', component: AboutComponent },
{ path: 'login', component: LoginComponent },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
