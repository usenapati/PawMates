import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetparentPetFormComponent } from './petparent-pet-form.component';

describe('PetparentPetFormComponent', () => {
  let component: PetparentPetFormComponent;
  let fixture: ComponentFixture<PetparentPetFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetparentPetFormComponent]
    });
    fixture = TestBed.createComponent(PetparentPetFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
