import { EventHelper } from './helpers/event.helper';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';
import { EventInput } from '@fullcalendar/core';
import { map } from 'rxjs/operators';
import { CalendarEvent } from '../models/events/calendar-event';

@Injectable({
  providedIn: 'root'
})
export class EventService extends BaseService {
  constructor(private httpClient: HttpClient, private eventHelper: EventHelper) {
    super();
  }

  public GetEvents(activeStart: Date, activeEnd: Date): Observable<EventInput[]> {
    const url = 'GetAll?start='
      + activeStart.toISOString()
      + '&end='
      + activeEnd.toISOString();

    return this.httpClient
      .get<CalendarEvent[]>(this.ConstructUrl(url))
      .pipe<EventInput[]>(map(events => {
        return this.eventHelper.ConvertToEventInput(events);
      }));
  }

  public CreateEvent(model: any): Observable<EventInput[]> {
    return this.httpClient
      .post<CalendarEvent>(this.ConstructUrl('CreateEvent'), model)
      .pipe<EventInput[]>(map(events => {
        return this.eventHelper.ConvertToEventInput(events);
      }));
  }

  protected get ApiRoute(): string {
    return '/api/Event/';
  }
}
