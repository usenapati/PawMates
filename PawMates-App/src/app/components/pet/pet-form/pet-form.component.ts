import { Component, OnInit, Input, Output, } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-pet-form',
  templateUrl: './pet-form.component.html',
  styleUrls: ['./pet-form.component.scss']
})
export class PetFormComponent implements OnInit {
  @Input() parent: any;
  newPet : any = { parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:''}
  constructor( private apiService: ApiService) { }
  ngOnInit(): void {
    this.newPet.parentId = this.parent.Id;
   }

  addPet(){
    if (this.newPet.name.trim() && this.newPet.petTypeId.trim() && this.newPet.breed && this.newPet.age ) {
      let petTypeId: number = parseInt(this.newPet.petTypeId);
      let age: number = parseInt(this.newPet.age);
      let petObj: any = {
        ...this.newPet,
        petTypeId: petTypeId,
        age: age
      }
      this.apiService.addPet(petObj)

      this.newPet = {  parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:''};
    };
  }
}
