import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

//Angular Material
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

//Model y DTOs
import { User } from '../../models/user.model';

//Dialogs
import { UserEditDialogComponent } from '../../components/user-edit-dialog/user-edit-dialog.component';
import { ConfirmDialogComponent } from '../../components/confirm-dialog/confirm-dialog.component';

//Services
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-list-users',
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.scss'
})
export class ListUsersComponent {
  private userService = inject(UserService);
  private snackBar = inject(MatSnackBar);
  private dialog = inject(MatDialog);

  users: User[] = [];
  displayedColumns: string[] = ['id', 'nombre', 'fechaNacimiento', 'sexo', 'acciones'];
  isLoading = true;

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.isLoading = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.snackBar.open('Error al cargar usuarios', 'Cerrar', { duration: 3000 });
      }
    });
  }

  deleteUser(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: {
        title: 'Eliminar Usuario',
        message: '¿Estás seguro de eliminar este usuario?',
      },
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (!confirmed) return;

      this.userService.deleteUser(id).subscribe({
        next: () => {
          this.snackBar.open('Usuario eliminado', 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-success'],
          });
          this.loadUsers();
        },
        error: () => {
          this.snackBar.open('Error al eliminar usuario', 'Cerrar', {
            duration: 3000,
            panelClass: ['snackbar-error'],
          });
        },
      });
    });
  }


  editUser(user: User) {
    const dialogRef = this.dialog.open(UserEditDialogComponent, {
      width: '600px',           
      maxWidth: '100vw',
      panelClass: 'custom-dialog',
      data: { ...user },
      disableClose: true
    });

    dialogRef.afterClosed().subscribe((result: User) => {
      if (result) {
        this.userService.updateUser(user.id, result).subscribe({
          next: () => {
            this.snackBar.open('Usuario editado correctamente', 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-success'],
            });
            this.loadUsers();
          },
          error: (err) => {
            this.snackBar.open('Error al editar el usuario', 'Cerrar', {
              duration: 3000,
              panelClass: ['snackbar-error'],
            });
            console.error('Error al actualizar', err)
          }
        });
      }
    });
  }

}