import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CalendarGridComponent } from './components/calendar-page/calendar-grid/calendar-grid.component';

const routes: Routes = [
  { path: '', component: CalendarGridComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
