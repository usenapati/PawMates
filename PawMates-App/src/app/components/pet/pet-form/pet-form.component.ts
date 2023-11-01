import { Component, OnInit, Input, Output, } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { Parent } from 'src/app/model/parent';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { PetDTO } from 'src/app/model/petDTO';


@Component({
  selector: 'app-pet-form',
  templateUrl: './pet-form.component.html',
  styleUrls: ['./pet-form.component.scss']
})
export class PetFormComponent implements OnInit {

  // petParent : Parent ={
  //   id: 0,
  //   firstName :'',
  //   lastName : '',
  //   email: '',
  //   phoneNumber: ''
  // } ;
  parentId : string = "";

  validationForm: FormGroup;
  constructor(private authService: AuthenticationService, private apiService: ApiService) {
    this.validationForm = new FormGroup({
      petTypeId: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      name : new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      breed :  new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      age: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      postalCode: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      imageUrl: new FormControl(null, { validators: Validators.required, updateOn: 'submit' })
    });
  }
  ngOnInit(): void {

    this.parentId = this.authService.getDecodedToken().PetParentId;

   }

   get name(): AbstractControl {
    return this.validationForm.get('name')!;
  }

  get petTypeId(): AbstractControl {
    return this.validationForm.get('petTypeId')!;
  }

  get breed(): AbstractControl {
    return this.validationForm.get('breed')!;
  }

  get age(): AbstractControl {
    return this.validationForm.get('age')!;
  }

  get postalCode(): AbstractControl {
    return this.validationForm.get('postalCode')!;
  }

  get imageUrl(): AbstractControl {
    return this.validationForm.get('imageUrl')!;
  }

  onSubmit(): void {
    this.validationForm.markAllAsTouched();
    if (this.validationForm.valid){
      let petObj: PetDTO  = {
        parentId : parseInt(this.parentId),
        name : this.name.value,
        petTypeId: parseInt(this.petTypeId.value),
        breed: this.breed.value,
        age: parseInt(this.age.value),
        postalCode: this.postalCode.value,
        imageUrl: this.imageUrl.value
      };
      this.apiService.addPet(petObj).subscribe(p => {
      });
      this.validationForm.reset();

    }
  }

  // addPet(){
  //   if (this.newPet.name.trim() && this.newPet.petTypeId.trim() && this.newPet.breed && this.newPet.age ) {
  //     let petTypeId: number = parseInt(this.newPet.petTypeId);
  //     let age: number = parseInt(this.newPet.age);
  //     let petObj: any = {
  //       ...this.newPet,
  //       petTypeId: petTypeId,
  //       age: age
  //     }
  //     this.apiService.addPet(petObj)

  //     this.newPet = {  parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:''};
  //   };
  // }
}
