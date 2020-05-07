import { EventInput } from '@fullcalendar/core';
import { EventService } from './../../services/event.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-calendar-event',
  templateUrl: './create-calendar-event.component.html',
  styleUrls: ['./create-calendar-event.component.css']
})
export class CreateCalendarEventComponent implements OnInit {
  @Output() canceled = new EventEmitter<boolean>();
  @Output() created = new EventEmitter<EventInput[]>();

  eventCreateForm: FormGroup;
  loading = false;
  submitted = false;
  isTimeDisabled = false;
  isCustomConfigVisible = false;
  frequencyModes = [
    { name: 'Once', value: 1},
    { name: 'Every day', value: 16},
    { name: 'Every week', value: 2},
    { name: 'Every month', value: 4},
    { name: 'Every year', value: 8},
    { name: 'Custom', value: 32}];
  week = [
    { name: 'Monday', partialName: 'Mn', value: 1, isActive: false },
    { name: 'Tuesday', partialName: 'Tu', valu: 2, isActive: false },
    { name: 'Wednesday', partialName: 'We', value: 3, isActive: false },
    { name: 'Thursday', partialName: 'Th', value: 4, isActive: false },
    { name: 'Friday', partialName: 'Fr', value: 5, isActive: false },
    { name: 'Saturday', partialName: 'Sa', value: 6, isActive: false },
    { name: 'Sunday', partialName: 'Su', value: 7, isActive: false }];

  constructor(private formBuilder: FormBuilder,
              private eventService: EventService) { }

  ngOnInit() {
    const currentDate = new Date();

    const options = {
      title: ['', Validators.required],
      description: [''],
      place: [''],
      startTime: [currentDate.toTimeString()],
      endTime: [currentDate.toTimeString()],
      isAllDay: [''],
      frequency: [''],
      startDate: [currentDate.toDateString(), Validators.required],
      endDate: [''],
      monday: [''],
      tuesday: [''],
      wednesday: [''],
      thursday: [''],
      friday: [''],
      saturday: [''],
      sunday: ['']
    };

    this.eventCreateForm = this.formBuilder.group(options);
  }

  OnSubmit() {
    this.submitted = true;
    this.loading = true;
    let model = this.ConvertFormToModel();
    this.eventService.CreateEvent(model)
      .subscribe(
        item => {
          this.created.emit(item);
        },
        error => {
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
  }

  OnCheckboxChange(checked: any) {
    this.isTimeDisabled = checked;
  }

  CancelClicked() {
    this.canceled.emit();
  }

  SelectChangeHandler(event: any) {
    this.isCustomConfigVisible = event.target.value === '32';
  }

  DaySelected(event: any) {
    this.week.map(item => {
      if (item.partialName === event) {
        item.isActive = !item.isActive;
      }
    });
  }

  get form() { return this.eventCreateForm.controls; }

  private ConvertFormToModel(): any {
    const values = this.eventCreateForm.value;
    const daysPerWeek = [];
    this.week.map(item => {
      if (item.isActive) {
        daysPerWeek.push(item.value);
      }
    });

    const result = {
      title: values.title,
      description: values.description,
      place: values.place,
      startTime: values.isAllDay ? '00:00' : values.startTime,
      endTime: values.isAllDay ? '23:59' : values.endTime,
      isAllDay: values.isAllDay? true : false,
      frequency: Number.parseInt(values.frequency, 10),
      startDate: values.startDate,
      endDate: values.endDate === '' ? null : values.endDate,
      days: daysPerWeek,
      backgroundColor: ''
    };

    return result;
  }
}
