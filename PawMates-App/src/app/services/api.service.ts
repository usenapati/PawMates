import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { RegisterModel } from '../model/registerModel';
import { Parent } from '../model/parent';
import { switchMap } from 'rxjs';

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

  // Pets
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

  // Pet Parent
  public addPetParent(PetParent: any) {
    return this.http.post<any>(baseUrl + '/parents', PetParent, {
      headers: this.getAuthHeaders()
    });
  }


  public login(username: string, password: string) {
    return this.http.post<{ token: any, parent: Parent }>(baseUrl + '/login', {
      id: 0,
      username: username,
      password: password
    });
  }

  public register(register: RegisterModel)
  {
    var petParentId = 0;
    var petParent = {
      firstName: register.firstName,
      lastName: register.lastName,
      email: register.email,
      phoneNumber: register.phoneNumber,
      imageUrl: register.profileImageURL
    };

    return this.addPetParent(petParent).pipe(
      switchMap(parent => {
        return this.http.post<{ token: any }>(baseUrl + '/register', {
            id: 0,
            petParentId: parent.id,
            username: register.userName,
            password: register.password
        }).pipe(
          switchMap(registeredUser => {
            return this.http.post<{ token: any }>(baseUrl + '/login', {
                username: register.userName,
                password: register.password
              }); 
          })
        );
      })
    );
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
