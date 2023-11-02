import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { Pet } from 'src/app/model/pet';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-petparent-profile',
  templateUrl: './petparent-profile.component.html',
  styleUrls: ['./petparent-profile.component.scss']
})
export class PetparentProfileComponent implements OnInit{
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

  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
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
}
