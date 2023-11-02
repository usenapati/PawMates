import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';


@Component({
  selector: 'app-pet-details',
  templateUrl: './pet-details.component.html',
  styleUrls: ['./pet-details.component.scss']
})
export class PetDetailsComponent  implements OnInit {
  pet : any = { parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:'', description: ''}
  petParent : any = {firstName:'', lastName:'', email:'', phoneNumber:'', imageUrl:'', description: ''};
  constructor(private route: ActivatedRoute, private apiService: ApiService) { }


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

  handleImageError(){
    this.pet.imageUrl = "../../assets/for-pet-without-image.png";
  }
}
