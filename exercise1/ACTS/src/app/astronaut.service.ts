import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map, catchError } from 'rxjs';
import { environment } from '../environments/environment';
import { AuthService } from './auth.service';

interface BaseResponse<T> {
  success: boolean;
  message: string;
  responseCode: number;
  people?: T;
  person?: T;
  astronautDuties?: T;
}

@Injectable({
  providedIn: 'root'
})
export class AstronautService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  getPeople(): Observable<any> {
    return this.http.get<BaseResponse<any>>(`${this.apiUrl}Person/GetPeople`, { headers: this.getHeaders() })
      .pipe(
        map(response => {
          console.log('GetPeople response:', response);
          if (response.success) {
            return response.people || [];
          } else {
            throw new Error(response.message || 'Failed to fetch people');
          }
        }),
        catchError(error => {
          console.error('Error in getPeople:', error);
          throw error;
        })
      );
  }

  getAstronautDutiesByName(name: string): Observable<any> {
    return this.http.get<BaseResponse<any>>(`${this.apiUrl}AstronautDuty/GetAstronautDutiesByName/${name}`, { headers: this.getHeaders() })
      .pipe(
        map(response => {
          console.log('GetAstronautDutiesByName response:', response);
          if (response.success) {
            // Check if astronautDuties exists, if not, check for person property
            if (response.astronautDuties) {
              return response.astronautDuties;
            } else if (response.person && response.person.astronautDuties) {
              return response.person.astronautDuties;
            } else {
              return []; // Return empty array if no duties found
            }
          } else {
            throw new Error(response.message || 'Failed to fetch astronaut duties');
          }
        }),
        catchError(error => {
          console.error('Error in getAstronautDutiesByName:', error);
          throw error;
        })
      );
  }
}
