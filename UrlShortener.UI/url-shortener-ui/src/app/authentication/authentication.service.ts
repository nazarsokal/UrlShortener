import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private baseApiUrl = 'http://localhost:5017/api/authentication';

  constructor(private httpClient: HttpClient) { }

  registerUser(userCredentials: any): Observable<any> {
    return this.httpClient.post(`${this.baseApiUrl}/register`, userCredentials);
  }

  loginUser(userCredentials: any): Observable<any> {
    return this.httpClient.post(`${this.baseApiUrl}/login`, userCredentials);
  }
}
