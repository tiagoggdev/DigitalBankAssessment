import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

//Angular Material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { provideNativeDateAdapter } from '@angular/material/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

//Services
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatSelectModule,
    MatButtonModule,
    MatSnackBarModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './create-user.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './create-user.component.scss'
})
export class CreateUserComponent {
  form!: FormGroup;
  private fb = inject(FormBuilder);
  private userService = inject(UserService);
  private snackBar = inject(MatSnackBar);

  isSubmitting = false;

  ngOnInit(): void {
    this.form = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(2)]],
      fechaNacimiento: ['', Validators.required],
      sexo: ['', Validators.required],
    });
  }
  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.userService
      .createUser(this.form.getRawValue())
      .subscribe({
        next: () => {
          this.snackBar.open('Usuario creado correctamente', 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-success'],
          });
          this.form.reset();
        },
        error: (err) => {
          console.error(err);
          this.snackBar.open('Error al crear el usuario', 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error'],
          });
        },
        complete: () => {
          this.isSubmitting = false;
        },
      });
  }
}
