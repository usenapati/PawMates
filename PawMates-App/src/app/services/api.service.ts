import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { RegisterModel } from '../model/registerModel';
import { Parent } from '../model/parent';
import { switchMap } from 'rxjs';
import { EventType } from '../model/eventtype';
import { Location } from '../model/location';
import { PlayDateDTO } from '../model/playdatedto';

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

  public getParentById(id: number) {
    return this.http.get<Parent>(`${baseUrl}/parents/${id}`);
  }

  public updateParent(id: number, parent: Parent) {
    return this.http.put<Parent>(`${baseUrl}/parents/${id}`, parent, {
      headers: this.getAuthHeaders(),
    });
  }

  public getParents() {
    return this.http.get<Parent[]>(baseUrl  + '/parents');
  }

  public getParentByPetId(id: number) {
    return this.http.get<Parent>(`${baseUrl}/pets/${id}/petparent`);
  }

  public getPetsByParent(parentId: number) {
    return this.http.get<any[]>(`${baseUrl}/parents/${parentId}/pets`);
  }

  // Login and Register
  public login(username: string, password: string) {
    return this.http.post<{ token: any }>(baseUrl + '/login', {
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
      imageUrl: register.profileImageURL,
      description: register.description,
      city: register.city,
      state: register.state,
      postalCode: register.postalCode
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

  

  // PlayDates
  public getPlayDates() {
    return this.http.get<PlayDateDTO[]>(baseUrl  + '/playdates');
  }

  public getPlayDateById(id: number) {
    return this.http.get<PlayDateDTO>(`${baseUrl}/playdates/${id}`);
  }

  public addPlayDate(PlayDate: any) {
    return this.http.post<any>(baseUrl + '/playdates', PlayDate, {
      headers: this.getAuthHeaders()
    });
  }

  public addPetToPlayDate(playDateId: number, petId: number) {
    return this.http.post<any>(`${baseUrl}/playdates/${playDateId}/pets/${petId}`, null, {
      headers: this.getAuthHeaders()
    });
  }

  public removePetFromPlayDate(playDateId: number, petId: number) {
    return this.http.delete<any>(`${baseUrl}/playdates/${playDateId}/pets/${petId}`, {
      headers: this.getAuthHeaders()
    });
  }

  // Events
  public getEvents() {
    return this.http.get<EventType[]>(baseUrl  + '/eventtype');
  }

  public getEventById(id: number) {
    return this.http.get<EventType>(`${baseUrl}/eventtype/${id}`);
  }

  public addEvent(Event: any) {
    return this.http.post<any>(baseUrl + '/eventtype', Event, {
      headers: this.getAuthHeaders()
    });
  }

  //Locations
  public getLocations() {
    return this.http.get<Location[]>(baseUrl  + '/locations');
  }

  public getLocationById(id: number) {
    return this.http.get<Location>(`${baseUrl}/locations/${id}`);
  }

  public addLocation(Location: any) {
    return this.http.post<any>(baseUrl + '/locations', Location, {
      headers: this.getAuthHeaders()
    });
  }

  public getPetTypes() {
    return this.http.get<any[]>(baseUrl  + '/pettypes');
  }

  public getRestrictionTypes() {
    return this.http.get<any[]>(baseUrl  + '/restrictiontypes');
  }

}
