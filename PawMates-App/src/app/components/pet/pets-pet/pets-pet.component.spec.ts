import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetsPetComponent } from './pets-pet.component';

describe('PetsPetComponent', () => {
  let component: PetsPetComponent;
  let fixture: ComponentFixture<PetsPetComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetsPetComponent]
    });
    fixture = TestBed.createComponent(PetsPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
