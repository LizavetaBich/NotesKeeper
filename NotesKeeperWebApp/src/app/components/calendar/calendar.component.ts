import { EventHelper } from './../../services/helpers/event.helper';
import { CreateCalendarEventComponent } from './../create-calendar-event/create-calendar-event.component';
import { EventService } from './../../services/event.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { EventInput, Calendar } from '@fullcalendar/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit, AfterViewInit {
  @ViewChild('calendar') fullCalendarComponent: FullCalendarComponent;
  @ViewChild('createCalendarEvent') createCalendarEventComponent: CreateCalendarEventComponent;

  private calendarApi: Calendar;
  private readonly viewModes = {
    viewCalendarMode: 0,
    eventCreateMode: 1,
    eventViewMode: 2
  };

  calendarPlugins = [dayGridPlugin, timeGridPlugin, interactionPlugin];
  events: EventInput[];
  isCreateEventMode: boolean;
  isViewEventMode: boolean;
  isCalendarMode: boolean;

  constructor(private eventService: EventService) {
    this.ChangeViewMode(this.viewModes.viewCalendarMode);
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.calendarApi = this.fullCalendarComponent.getApi();
    this.UpdateEvents();
  }

  gotoPast() {
    this.calendarApi.gotoDate('2000-01-01'); // call a method on the Calendar object
  }

  next() {
    this.calendarApi.next();
    this.UpdateEvents();
  }

  prev() {
    this.calendarApi.prev();
    this.UpdateEvents();
  }

  public HandleDateClick(arg) {
    this.ChangeViewMode(this.viewModes.eventCreateMode);
  }

  onEventCreateCanceled() {
    this.ChangeViewMode(this.viewModes.viewCalendarMode);
  }

  onEventCreated(models: EventInput[]) {
    this.events = this.events.concat(models);
    this.ChangeViewMode(this.viewModes.viewCalendarMode);
  }

  private get View() {
    return this.calendarApi.view;
  }

  private UpdateEvents(): void {
    this.eventService.GetEvents(this.View.activeStart, this.View.activeEnd)
      .subscribe(models => {
        this.events = models;
      });
  }

  private ChangeViewMode(mode: number): void {
    this.isCalendarMode = mode === this.viewModes.viewCalendarMode;
    this.isCreateEventMode = mode === this.viewModes.eventCreateMode;
    this.isViewEventMode = mode === this.viewModes.eventViewMode;
  }
}
