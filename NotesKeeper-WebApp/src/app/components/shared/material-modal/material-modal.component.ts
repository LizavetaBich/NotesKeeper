import { Component, OnInit, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-material-modal',
  templateUrl: './material-modal.component.html',
  styleUrls: ['./material-modal.component.sass']
})
export class MaterialModalComponent implements OnInit {
  template!: TemplateRef<any>;

  constructor() {}

  ngOnInit(): void {
  }
}
