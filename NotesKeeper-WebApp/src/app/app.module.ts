import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarGridComponent } from './components/calendar-page/calendar-grid/calendar-grid.component';
import { MaterialModalComponent } from './components/shared/material-modal/material-modal.component';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { VarDirective } from './directives/var-directive.directive';

@NgModule({
  declarations: [
    AppComponent,
    CalendarGridComponent,
    MaterialModalComponent,
    VarDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
    BrowserAnimationsModule,
    MatToolbarModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    FormsModule
  ],
  providers: [ MatDialog ],
  bootstrap: [AppComponent],
  exports: [
    FormsModule
  ]
})
export class AppModule { }
