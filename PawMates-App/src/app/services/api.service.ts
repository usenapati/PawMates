import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { RegisterModel } from '../model/registerModel';
import { Parent } from '../model/parent';

const baseUrl = 'https://localhost:7136/gateway';
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


  public getPets() {
    return this.http.get<any[]>(baseUrl  + '/pets');
  }

  public addPet(Pet: any) {
    return this.http.post<any>(baseUrl + '/pets', Pet, {
      headers: this.getAuthHeaders()
    });
  }

  public getPetById(id: number) {
    return this.http.get<any>(baseUrl + `/pets/${id}`);
  }

  public updatePet(id: number, Pet: any) {
    return this.http.put<any>(baseUrl + `/pets/${id}`, Pet, {
      headers: this.getAuthHeaders()
    });
  }

  public deletePetById(id: number) {
    return this.http.delete<any>(baseUrl + `/pets/${id}`, {
      headers: this.getAuthHeaders()
    });
  }

  public getPetsParent(id: number) {
    return this.http.get<any>(baseUrl + `/pets/${id}/petparent`);
  }


  public login(username: string, password: string) {
    return this.http.post<{ token: any }>(baseUrl + '/login', {
      id: 0,
      username: username,
      password: password
    });
  }

  public register(register: RegisterModel)
  {
    // Create a Pet Parent
    // Create User
    // Login to new User
    return this.http.post<{ token: any }>(baseUrl + '/login', {
      id: 0,
      username: register.userName,
      password: register.password
    });
  }

  public getParentById(id: number) {
    return this.http.get<Parent>(`${baseUrl}/parents/${id}`);
  }

  public updateParent(id: number, parent: Parent) {
    return this.http.put<Parent>(`${baseUrl}/parents/${id}`, parent, {
      headers: this.getAuthHeaders(),
    });
  }

  public getPetsByParent(parentId: number) {
    return this.http.get<any[]>(`${baseUrl}/parents/${parentId}/pets`);
  }
}
