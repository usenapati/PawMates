import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';


const baseUrl = 'https://localhost:7136/gateway';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http:HttpClient,  private authService: AuthenticationService) { }

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

  public getPetParent(id: number) {
    return this.http.get<any>(baseUrl + `/pets/${id}/petparent`);
  }

}
