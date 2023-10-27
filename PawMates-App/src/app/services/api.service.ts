import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { RegisterModel } from '../model/registerModel';

const baseUrl = 'https://localhost:7136';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient, private authService: AuthenticationService) { }


  private getAuthHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });
  }

  public login(username: string, password: string) {
    return this.http.post<{ token: any }>(baseUrl + '/gateway/login', {
      username: username,
      password: password
    });
  }

  public register(register: RegisterModel)
  {
    
  }
}
