import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private jwtToken: string | null = null;
  constructor() { }

  public setToken(token: string): void{
    this.jwtToken = token;
    localStorage.setItem('token', token);
  }

  public getToken(): string | null{
    if(!this.jwtToken){
      this.jwtToken = localStorage.getItem('token');
    }
    return this.jwtToken;
  }

  public clearToken(): void {
    this.jwtToken = null;
  }

  public isAuthenticated(): boolean {
    return !!this.jwtToken;
  }
}
