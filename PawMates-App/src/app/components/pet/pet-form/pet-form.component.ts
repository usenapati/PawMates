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

//   @Input() parent: any;
//   newPet : any = { parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:'', description: ''}
  petTypeId2! : number;
  parentId : string = "";
  petTypes : any[] = [];
  validationForm: FormGroup;
  constructor(private authService: AuthenticationService, private apiService: ApiService) {
    this.validationForm = new FormGroup({
      petTypeId: new FormControl(1, { validators: Validators.required, updateOn: 'submit' }),
      name : new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      breed :  new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      age: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.pattern("^[0-9]*$"), Validators.min(0), Validators.max(100)]), updateOn: 'submit' }),
      postalCode: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(10)]), updateOn: 'submit' }),
      imageUrl: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(255)]), updateOn: 'submit' }),
      description: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(255)]), updateOn: 'submit' }),
    });

  }

  ngOnInit(): void {

    this.parentId = this.authService.getDecodedToken().PetParentId;
    this.apiService.getPetTypes().subscribe(petTypes =>{
      this.petTypes = petTypes;
    });
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

  get description(): AbstractControl {
    return this.validationForm.get('description')!;
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
        imageUrl: this.imageUrl.value,
        description: this.description.value
      };
      this.apiService.addPet(petObj).subscribe(p => {
      });
      this.validationForm.reset();

    }
  }

}
