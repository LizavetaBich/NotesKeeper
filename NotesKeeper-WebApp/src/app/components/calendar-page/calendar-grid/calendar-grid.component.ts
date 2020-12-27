import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef
} from '@angular/core';
import {
  CalendarView,
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent
} from 'angular-calendar';
import {
  Subject
} from 'rxjs';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from 'date-fns';
import { MatDialog } from '@angular/material/dialog';
import { EventViewComponent } from '../event-view/event-view.component';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
  white: {
    primary: '#FFFFFF',
    secondary: '#FFFFFF'
  }
};

@Component({
  selector: 'app-calendar-grid',
  templateUrl: './calendar-grid.component.html',
  styleUrls: ['./calendar-grid.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CalendarGridComponent implements OnInit {

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  event!: CalendarEvent;

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [
    {
      start: startOfDay(new Date()),
      title: 'An event with no end date',
      color: colors.yellow,
      meta: ''
    }
  ];

  activeDayIsOpen: boolean = true;

  constructor(private modal: MatDialog) {}

  ngOnInit(): void {
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.event = event;
    const dialogRef = this.modal.open(EventViewComponent, {
      height: '75%',
      width: '50%',
      data: event
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.addEvent(result);
      }
    });
  }

  addEvent(event: CalendarEvent): void {
    this.events = [
      ...this.events,
      event
    ];
  }

  emptyEvent(day: Date | undefined): CalendarEvent {
    return {
      title: '',
      start: startOfDay(day || new Date()),
      end: endOfDay(day || new Date()),
      color: colors.white,
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      meta: {
        description: ''
      }
    }
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
