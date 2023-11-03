import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { Pet } from 'src/app/model/pet';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';


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
  validationForm: FormGroup;
  constructor(private route: ActivatedRoute, private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.validationForm = new FormGroup({
      firstName: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      lastName : new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      email :  new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      phoneNumber: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(15)]), updateOn: 'submit' }),
      city :  new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      state :  new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(50)]), updateOn: 'submit' }),
      postalCode: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(15)]), updateOn: 'submit' }),
      imageUrl: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(255)]), updateOn: 'submit' }),
      description: new FormControl(null, { validators: Validators.compose([Validators.required, Validators.maxLength(255)]), updateOn: 'submit' }),
    });
   }

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

  deletePet(pet: any) {
    this.apiService.deletePetById(pet.id)
    .subscribe({
      next: (response) => {
        console.log('Deleting....');
        this.router.navigate(['profile']);
        location.reload();
      }
    });
  }

  handleImageError(pet :any){
    pet.imageUrl = "../../assets/for-pet-without-image.png";
  }

  // Form Validation
  get firstName(): AbstractControl {
    return this.validationForm.get('firstName')!;
  }

  get lastName(): AbstractControl {
    return this.validationForm.get('lastName')!;
  }

  get email(): AbstractControl {
    return this.validationForm.get('email')!;
  }

  get phoneNumber(): AbstractControl {
    return this.validationForm.get('phoneNumber')!;
  }

  get city(): AbstractControl {
    return this.validationForm.get('city')!;
  }

  get state(): AbstractControl {
    return this.validationForm.get('state')!;
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
      let parentObj: Parent = {
        id: this.parent.id,
        firstName: this.firstName.value,
        lastName: this.lastName.value,
        email: this.email.value,
        phoneNumber: this.phoneNumber.value,
        imageUrl: this.imageUrl.value,
        description: this.description.value,
        city: this.city.value,
        state: this.state.value,
        postalCode: this.postalCode.value
      };
      
      this.apiService.updateParent(this.parent.id, parentObj)
      .subscribe({
        next: (response) => {
          console.log('Editing....');
          this.router.navigate(['profile']);
          location.reload();
        }
      });
      this.validationForm.reset();
    }
}

handleParentImageError() {
  this.parent.imageUrl = "../../assets/for-parent-without-image.png";

}

}
