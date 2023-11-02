import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { Pet } from 'src/app/model/pet';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-petparent-detail',
  templateUrl: './petparent-detail.component.html',
  styleUrls: ['./petparent-detail.component.css']
})
export class PetparentDetailComponent implements OnInit {
  pets: Pet[] = [];
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
  pet: Pet = {
    id: 0,
    parentId : 0,
    petTypeId: 0, 
    name: '', 
    breed: '', 
    age: 0, 
    postalCode: '', 
    imageUrl: '',
    description: ''
  };
  isEditing = false;

  constructor(private route: ActivatedRoute, private authService: AuthenticationService, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    const id = this.authService.getDecodedToken().PetParentId;
    if (id) {
      this.apiService.getParentById(+id)
      .subscribe({
        next: (response) => {
          this.parent = response;
        }
      });

      this.apiService.getPetsByParent(+id)
      .subscribe({
        next: (pets: Pet[]) => {
          this.pets = pets;
      }})
    }
  }

  edit() {
    this.isEditing = true;
  }
  
  editParent() {
    this.apiService.updateParent(this.parent.id, this.parent)
    .subscribe({
      next: (response) => {
        console.log('Editing....');
        this.router.navigate(['profile']);
        location.reload();
      }
    });
  }

  deletePet() {
    this.apiService.deletePetById(this.pet.id)
    .subscribe({
      next: (response) => {
        console.log('Deleting....');
        this.router.navigate(['profile']);
        location.reload();
      }
    });
  }
}
