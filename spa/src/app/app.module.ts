import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CarouselModule} from 'ngx-bootstrap/carousel';


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
import { UnitPicturesComponent } from './unitPictures/unitPictures.component';

@NgModule({
  declarations: [						
    AppComponent,
      NavBarComponent,
      HomeComponent,
      ContactComponent,
      AboutComponent,
      LoginComponent,
      UnitPicturesComponent
   ],
  imports: [
    CarouselModule.forRoot(),
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
