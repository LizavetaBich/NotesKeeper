import { CreateCalendarEventComponent } from './components/create-calendar-event/create-calendar-event.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AuthGuard } from './guards/auth.guard';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  { path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard] },
  { path: 'calendar/createEvent', component: CreateCalendarEventComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },

  // otherwise redirect to home
  { path: '**', redirectTo: 'calendar' }];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
