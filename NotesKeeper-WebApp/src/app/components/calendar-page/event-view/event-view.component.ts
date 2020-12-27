import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CalendarEvent } from 'angular-calendar';
import { DateHelperService } from 'src/app/helpers/date-helper.service';
import {DomSanitizer} from '@angular/platform-browser';
import {MatIconRegistry} from '@angular/material/icon';

@Component({
  selector: 'app-event-view',
  templateUrl: './event-view.component.html',
  styleUrls: ['./event-view.component.sass']
})
export class EventViewComponent implements OnInit {

  event: CalendarEvent;

  eventStartTime!: Date;

  eventEndTime!: Date;

  isAllDayEvent: boolean = false;

  dateHelper: DateHelperService;

  constructor(private helper: DateHelperService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private dialogRef: MatDialogRef<EventViewComponent>,
    @Inject(MAT_DIALOG_DATA) event: CalendarEvent) {
      this.event = event;
      this.dateHelper = helper;
      this.iconRegistry.addSvgIcon('schedule',
        this.sanitizer.bypassSecurityTrustResourceUrl('assets/svg/schedule.svg'));
    }

  ngOnInit(): void {
    this.eventStartTime = this.event.start;
    this.eventEndTime = this.event.end || new Date();
  }

  startDayFilter = (d: Date | null): boolean => {
    const date = d || new Date();
    return this.dateHelper.compareDatePartAbsolute(date, new Date()) !== -1;
  }

  endDayFilter = (d: Date | null): boolean => {
    const date = d || new Date();
    return this.dateHelper.compareDatePartAbsolute(date, new Date()) !== -1;
  }

  allDayChecked(isAllDayChecked: boolean) {
    this.isAllDayEvent = isAllDayChecked;

    if (isAllDayChecked) {
      this.event.end = this.event.start;
    }
  }

  closeModal() {
    this.dialogRef.close();
  }

  saveAndClose() {
    this.event.start = this.helper.attachTime(this.event.start, this.eventStartTime);
    this.event.end = this.helper.attachTime(this.event.end || this.event.start, this.eventEndTime);

    this.dialogRef.close(this.event);
  }
}
