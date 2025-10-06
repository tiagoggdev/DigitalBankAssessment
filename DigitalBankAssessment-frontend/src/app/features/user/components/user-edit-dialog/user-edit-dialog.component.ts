import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

//Angular Material
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import {MatDividerModule} from '@angular/material/divider';
import {MatDatepickerModule} from '@angular/material/datepicker';

//Dtos
import { UserToEdit } from '../../dtos/userToEdit';

export interface UserDialogData {
  id: number;
  nombre: string;
  fechaNacimiento: string;
  sexo: string;
}

@Component({
  selector: 'app-user-edit-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDividerModule,
    MatDatepickerModule,
  ],
  templateUrl: './user-edit-dialog.component.html',
  styleUrl: './user-edit-dialog.component.scss'
})
export class UserEditDialogComponent {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<UserEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDialogData
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      nombre: [this.data?.nombre || '', Validators.required],
      fechaNacimiento: [this.data?.fechaNacimiento || '', Validators.required],
      sexo: [this.data?.sexo || '', Validators.required],
    });
  }

  onSave() {
    if (this.form.valid) {
      const rawValue = this.form.value;

      const userToEdit: UserToEdit = {
        nombre: rawValue.nombre ?? '',
        fechaNacimiento: new Date(rawValue.fechaNacimiento ?? new Date()),
        sexo: (rawValue.sexo as 'M' | 'F' | '') ?? '',
      };

      this.dialogRef.close(userToEdit);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
