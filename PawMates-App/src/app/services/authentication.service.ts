import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private jwtToken: string | null = null;

  constructor() { }

  public setToken(token: string): void {
    this.jwtToken = token;
    // store token in localStorage under the of token
    localStorage.setItem('token', token);
  }

  public getToken(): string | null {
    if (!this.jwtToken) {
      // get token from localStorage

      this.jwtToken = localStorage.getItem('token');
    }
    return this.jwtToken;
  }

  public clearToken(): void {

    this.jwtToken = null;
    localStorage.removeItem('token');
  }

  public isAuthenticated(): boolean {

    return !!this.getToken();

  }

  public getDecodedToken(): any {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    return jwtDecode(token);
  }
}
