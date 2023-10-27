import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetparentPetComponent } from './petparent-pet.component';

describe('PetparentPetComponent', () => {
  let component: PetparentPetComponent;
  let fixture: ComponentFixture<PetparentPetComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetparentPetComponent]
    });
    fixture = TestBed.createComponent(PetparentPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
