import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FullCalendarModule } from '@fullcalendar/angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { JwtTokenInterceptor } from './interceptors/jwt-token.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { CreateCalendarEventComponent } from './components/create-calendar-event/create-calendar-event.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    CalendarComponent,
    CreateCalendarEventComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FullCalendarModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtTokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
