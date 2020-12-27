import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateHelperService {

  constructor() { }

  public toDate(value: string) : Date {
    return new Date(value);
  }

  public toTimeString(value: Date | undefined) : string {
    if (!value) {
      return '';
    }
    const hours = value.getHours();
    const minutes = value.getMinutes();

    return `${hours === 0 ? '00' : hours}:${minutes === 0 ? '00' : minutes}`;
  }

  public attachTimeString(date: Date, time: string) : Date {
    const timeParts = time.split(':');
    const hours = parseInt(timeParts[0]);
    const minutes = parseInt(timeParts[1]);
    return new Date(date.getFullYear(), date.getMonth(), date.getDate(), hours, minutes);
  }

  public attachTime(date: Date, time: Date) : Date {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate(), time.getHours(), time.getMinutes());
  }

  public compareDatePartAbsolute(d1: Date, d2: Date) : number {
    if (d1.getDate() < d2.getDate() && this.compareMonthAbsolute(d1, d2) <= 0) {
      return -1;
    } else if (d1.getDate() === d2.getDate() && this.compareMonthAbsolute(d1, d2) === 0) {
      return 0;
    } else {
      return 1;
    }
  }

  private compareYearAbsolute(d1: Date, d2: Date) : number {
    if (d1.getFullYear() < d2.getFullYear()) {
      return -1;
    } else if (d1.getFullYear() === d2.getFullYear()) {
      return 0;
    } else {
      return 1;
    }
  }

  private compareMonthAbsolute(d1: Date, d2: Date) : number {
    if (d1.getMonth() < d2.getMonth() && this.compareYearAbsolute(d1, d2) <= 0) {
      return -1;
    } else if (d1.getMonth() === d2.getMonth() && this.compareYearAbsolute(d1, d2) === 0) {
      return 0;
    } else {
      return 1;
    }
  }
}
