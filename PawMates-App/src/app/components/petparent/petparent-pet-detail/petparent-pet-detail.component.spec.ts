import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetparentPetDetailComponent } from './petparent-pet-detail.component';

describe('PetparentPetDetailComponent', () => {
  let component: PetparentPetDetailComponent;
  let fixture: ComponentFixture<PetparentPetDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetparentPetDetailComponent]
    });
    fixture = TestBed.createComponent(PetparentPetDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
