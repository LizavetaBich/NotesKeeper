import { CalendarEvent } from '../../models/events/calendar-event';
import { Injectable } from '@angular/core';
import { EventInput } from '@fullcalendar/core';

@Injectable({
  providedIn: 'root'
})
export class EventHelper {
  public ConvertToEventInput(calendarEvent: CalendarEvent | CalendarEvent[]): EventInput[] {
    if (calendarEvent instanceof CalendarEvent) {
      return this.ConvertOneEventToEventInput(calendarEvent);
    } else {
      return this.ConvertEventArrayToEventInput(calendarEvent);
    }
  }

  private ConvertOneEventToEventInput(calendarEvent: CalendarEvent): EventInput[] {
    return calendarEvent.days.map(day => {

      return {
        groupId: calendarEvent.id,
        allDay: calendarEvent.allDay,
        start: this.ConstructDate(day, calendarEvent.eventStartTime),
        end: this.ConstructDate(day, calendarEvent.eventEndTime),
        title: calendarEvent.name,
        editable: true,
        startEditable: true,
        durationEditable: true,
        backgroundColor: calendarEvent.backgroundColor,
        borderColor: calendarEvent.backgroundColor
      };
    });
  }

  private ConvertEventArrayToEventInput(calendarEvents: CalendarEvent[]): EventInput[] {
    let result = new Array<EventInput>();

    for (const calendarEvent of calendarEvents) {
      result = result.concat(this.ConvertOneEventToEventInput(calendarEvent));
    }

    return result;
  }

  private ConstructDate(day: string, time: string): Date {
    if (!(day && time)) {
      return null;
    }

    const dayDate = new Date(Date.parse(day));
    const timeDate = new Date(Date.parse(time));

    return new Date(dayDate.getUTCFullYear(),
      dayDate.getUTCMonth(),
      dayDate.getUTCDate(),
      timeDate.getHours(),
      timeDate.getMinutes(),
      timeDate.getSeconds(),
      timeDate.getMilliseconds());
  }
}
