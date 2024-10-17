import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';

export interface LoginResponse {
  success: boolean;
  message: string;
  token: string;
  exception?: any;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl; // Use the API URL from environment config
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}Auth/login`, { UserName: username, Password: password }, { withCredentials: true })
      .pipe(
        tap(response => {
          if (response.success) {
            this.setToken(response.token);
          }
        })
      );
  }

  register(firstName: string, lastName: string, email: string, username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}Auth/register`, { firstName, lastName, email, username, password });
  }

  setToken(token: string): void {

    sessionStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }

  logout(): void {
    sessionStorage.removeItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
