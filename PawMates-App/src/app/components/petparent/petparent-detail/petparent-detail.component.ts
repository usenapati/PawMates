import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { Pet } from 'src/app/model/pet';
import { ApiService } from 'src/app/services/api.service';

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
    imageUrl: ''
  };
  isEditing = false;

  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    //const id = this.route.snapshot.paramMap.get('id');
    const id = 6;
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
      }
    });
  }
}
