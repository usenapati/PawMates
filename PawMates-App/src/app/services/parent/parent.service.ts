import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication.service';
import { Parent } from 'src/app/model/parent';

const baseParentUrl = 'https://localhost:7136/gateway/parents';

@Injectable({
  providedIn: 'root'
})
export class ParentService {

  constructor(private http: HttpClient, private authService: AuthenticationService) { }

  private getAuthHeaders(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.authService.getToken()}`,
    });
  }

  public getParents() {
    return this.http.get<Parent[]>(baseParentUrl);
  }

  public getParentById(id: any) {
    return this.http.get<Parent>(`${baseParentUrl}/${id}`);
  }

  public addParent(parent: Parent) {
    return this.http.post<Parent>(baseParentUrl, parent, {
      headers: this.getAuthHeaders(),
    });
  }

  public updateParent(id: number, parent: Parent) {
    return this.http.put<Parent>(`${baseParentUrl}/${id}`, parent, {
      headers: this.getAuthHeaders(),
    });
  }

  public deleteParent(id: number) {
    return this.http.delete<Parent>(`${baseParentUrl}/${id}`, {
      headers: this.getAuthHeaders(),
    });
  }

  public getPets(parentId: number) {
    
  }

  public addPet() {

  }

  public deletePet(parentId: number, petId: number) {

  }
}
