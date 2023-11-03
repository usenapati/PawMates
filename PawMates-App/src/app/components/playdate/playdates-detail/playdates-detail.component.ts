import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { forkJoin } from 'rxjs';
import { Parent } from 'src/app/model/parent';
import { Pet } from 'src/app/model/pet';
import { PlayDateDTO } from 'src/app/model/playdatedto';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-playdates-detail',
  templateUrl: './playdates-detail.component.html',
  styleUrls: ['./playdates-detail.component.css']
})
export class PlaydatesDetailComponent implements OnInit {
  playDate: PlayDateDTO = {
    id: 0,
    hostId: 0,
    hostName: '',
    locationName: '',
    address: '',
    city: '',
    state: '',
    postalCode: '',
    eventName: '',
    eventDescription: '',
    startTime: new Date,
    endTime: new Date,
    numberOfPets: 0,
    pets: []
  };
  parent: Parent = {
    id: 0,
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    imageUrl: '',
    description: '',
    city: '',
    state: '',
    postalCode: ''
  };
  userPetsOnPlayDate: Pet[] = [];
  userPetList: any[] = [];
  selectedUserPets: any[] = [];
  // Host Pets
  hostPets: Pet[] = [];
  hostPetList: any[] = [];
  selectedHostPets: any[] = [];
  hostPetDropdownSettings: IDropdownSettings = {};

  isEditing = false;
  isDelete = false;
  isAdd = false;
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router, private authService: AuthenticationService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.apiService.getPlayDateById(+id)
      .subscribe({
        next: (response) => {
          this.playDate = response;
      }});
    }

    // Get Pet Parent Id from Token
    this.parent.id = this.authService.getDecodedToken().PetParentId;

    // Get Logged In Parent's Pets
    forkJoin({
      hostPets: this.apiService.getPetsByParent(+this.parent.id),
    }).subscribe(result => {
      this.hostPets = result.hostPets;
      this.hostPetList = this.hostPets.map(p => ({pet_id: p.id, pet_name: p.name}));
    });
 
    this.hostPetDropdownSettings = {
      singleSelection: false,
      idField: 'pet_id',
      textField: 'pet_name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
    };
  }

  delete() {
    this.isEditing = true;
    this.isDelete = true;
    this.myPetsOnPlayDate();
    this.userPetList = this.userPetsOnPlayDate.map(p => ({pet_id: p.id, pet_name: p.name}));
  }
  add() {
    this.isEditing = true;
    this.isAdd = true;
  }

  myPetsOnPlayDate() {
    for (let i = 0; i < this.playDate.pets.length; i++) {
      for (let j = 0; j < this.hostPets.length; j++) {
        if (this.playDate.pets[i].id === this.hostPets[j].id) {
          this.userPetsOnPlayDate.push(this.hostPets[j])
        }
      }
    }
    console.log(this.userPetsOnPlayDate)
    return this.userPetsOnPlayDate;
  }

  addPet() {
    // Pets IDs - Loop through each id and add it to the submitted play date
    this.selectedHostPets.forEach(element => {
      // Add Pet to Play Date
      this.apiService.addPetToPlayDate(this.playDate.id, element.pet_id)
      .subscribe(response => {
        console.log(response);
        this.router.navigate(['playdates']);
      });
    });
  }

  removePet() {
    for (let i = 0; i < this.playDate.pets.length; i++) {
      for (let j = 0; j < this.hostPets.length; j++) {
        let petId = this.hostPets[j].id;
        if (this.playDate.pets[i].id === this.hostPets[j].id) {
          this.apiService.removePetFromPlayDate(this.playDate.id, petId)
          .subscribe(response => {
        console.log(response);
        this.router.navigate(['playdates']);
      });
        }
      }
    }
  }
}
