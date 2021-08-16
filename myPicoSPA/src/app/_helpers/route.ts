import { Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { PicturesComponent } from '../pictures/pictures.component';
import { ScheduleComponent } from '../schedule/schedule.component';
import { TodoComponent } from '../todo/todo.component';
// import { ScheduleResolver } from '../_resolvers/Schedule.resolver';
import { AuthGuard } from '../_guards/AuthGuard';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { UserEditComponent } from '../users/user-edit/user-edit.component';
import { UserEditResolver } from '../_resolvers/UserEditResolver';
import { PreventUnsavedChanges } from '../_guards/prevent-unsaved-changes.guard';
import { UnitDetailsComponent } from '../unit/unit-details/unit-details.component';
import { AppointmentComponent } from '../users/appointments/appointment.component';
import { ApptListResolver } from '../_resolvers/ApptListResolver';
import { RulesComponent } from '../rules/rules.component';
import { AccessibilityComponent } from '../accessibility/accessibility.component';
import { PicoGeneralComponent } from '../pico/pico-general/pico-general.component';
import { PicoMapsComponent } from '../pico/pico-maps/pico-maps.component';
import { PicoImagesComponent } from '../pico/pico-images/pico-images.component';
import { PicoRulesComponent } from '../pico/pico-rules/pico-rules.component';
import { UserDetailComponent } from '../users/user-detail/user-detail.component';
import { UserDetailResolver } from '../_resolvers/UserDetailResolver';
import { ConfigComponent } from '../config/config.component';


export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent},
    { path: 'register', component: RegisterComponent},
    { path: 'images', component: PicturesComponent },
    { path: 'rules', component: RulesComponent },
    { path: 'unit/:id', component: PicturesComponent },
    { path: 'todo', component: TodoComponent},
    { path: 'unitDetails/:id', component: UnitDetailsComponent},
    { path: 'accessibility', component: AccessibilityComponent},
    { path: 'schedule', component: ScheduleComponent},
    { path: 'picoGeneral', component: PicoGeneralComponent},
    { path: 'picoMaps', component: PicoMapsComponent},
    { path: 'picoImages', component: PicoImagesComponent},
    { path: 'picoRules', component: PicoRulesComponent},
    { path: 'config', component: ConfigComponent},


    {
      path: '',
       runGuardsAndResolvers: 'always',
      // canActivate: [AuthGuard],
      children: [
        // { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver} },
        // { path: 'member/edit', component: MemberEditComponent,
        // resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges] },
        { path: 'userDetails/:id', component: UserDetailComponent, resolve: {user: UserDetailResolver} },
        { path: 'user/edit', component: UserEditComponent, resolve: {user: UserEditResolver}, canDeactivate: [PreventUnsavedChanges] },
        { path: 'booking', component: AppointmentComponent, resolve: {appointments: ApptListResolver}}
      ]
    },

    { path: '**', redirectTo: 'home', pathMatch: 'full' }
  ];

