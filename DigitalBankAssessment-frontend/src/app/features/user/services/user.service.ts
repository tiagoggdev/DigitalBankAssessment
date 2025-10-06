import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

//Config
import { ApiEndpoints } from '../../../core/config/api-endpoints';

//Models y DTOs
import { User } from '../models/user.model';
import { ApiResponse } from '../../../core/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);

  getUsers(): Observable<User[]> {
    return this.http.get<ApiResponse<User[]>>(ApiEndpoints.users).pipe(
      map((response) => response.data || [])
    );
  }

  createUser(user: Partial<User>): Observable<ApiResponse<null>> {
    return this.http.post<ApiResponse<null>>(ApiEndpoints.users, user);
  }

  updateUser(id: number, user: Partial<User>): Observable<ApiResponse<null>> {
    return this.http.put<ApiResponse<null>>(`${ApiEndpoints.users}/${id}`, user);
  }

  deleteUser(id: number): Observable<ApiResponse<null>> {
    return this.http.delete<ApiResponse<null>>(`${ApiEndpoints.users}/${id}`);
  }
}