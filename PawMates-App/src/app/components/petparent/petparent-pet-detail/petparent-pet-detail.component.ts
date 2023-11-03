import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PetDTO } from 'src/app/model/petDTO';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-petparent-pet-detail',
  templateUrl: './petparent-pet-detail.component.html',
  styleUrls: ['./petparent-pet-detail.component.css']
})
export class PetparentPetDetailComponent implements OnInit {
  pet : any = { parentId : '', petTypeId: '', name: '', breed: '', age: '', postalCode:'', imageUrl:'', description: ''}
  petParent : any = {firstName:'', lastName:'', email:'', phoneNumber:'', imageUrl:'', description: ''};
  petTypes : any[] = [];
  validationForm: FormGroup;
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) {
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
        parentId : this.pet.parentId,
        name : this.name.value,
        petTypeId: parseInt(this.petTypeId.value),
        breed: this.breed.value,
        age: parseInt(this.age.value),
        postalCode: this.postalCode.value,
        imageUrl: this.imageUrl.value,
        description: this.description.value
      };
      this.apiService.updatePet(this.pet.id, petObj)
      .subscribe({
      next: (response) => {
        console.log('Editing....');
        this.router.navigate(['profile']);
      }
    });
      this.validationForm.reset();

    }
  }

}
