import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-petparent-pet-detail',
  templateUrl: './petparent-pet-detail.component.html',
  styleUrls: ['./petparent-pet-detail.component.css']
})
export class PetparentPetDetailComponent implements OnInit {
  pet : any = { parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:'', description: ''}
  petParent : any = {firstName:'', lastName:'', email:'', phoneNumber:'', imageUrl:'', description: ''};
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.apiService.getPetById(+id).subscribe(pet => {
        this.pet = pet;
      });

      this.apiService.getPetsParent(+id).subscribe(petParent =>{
        this.petParent = petParent;
      })
    }
  }

  editPet() {
    this.apiService.updatePet(this.pet.id, this.pet)
    .subscribe({
      next: (response) => {
        console.log('Editing....');
        this.router.navigate(['profile']);
      }
    });
  }

  handleImageError(){
    this.pet.imageUrl = "../../assets/for-pet-without-image.png";
  }


}
