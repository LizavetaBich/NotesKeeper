import { Component, OnInit, Inject } from '@angular/core';

interface DialogData {
  email: string;
}

@Component({
  selector: 'app-material-modal',
  templateUrl: './material-modal.component.html',
  styleUrls: ['./material-modal.component.sass']
})
export class MaterialModalComponent implements OnInit {

  constructor(/* public dialogRef: MatDialogRef<MaterialModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData */) { }

  onNoClick(): void {
    // this.dialogRef.close();
  }

  ngOnInit(): void {
  }

}
