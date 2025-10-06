import { Routes } from '@angular/router';
import { CreateUserComponent } from './features/user/pages/create-user/create-user.component';
import { ListUsersComponent } from './features/user/pages/list-users/list-users.component';

export const routes: Routes = [
  { path: '', redirectTo: 'usuario/crear', pathMatch: 'full' },
  { path: 'usuario/crear', component: CreateUserComponent },
  { path: 'usuario/listar', component: ListUsersComponent }
];

