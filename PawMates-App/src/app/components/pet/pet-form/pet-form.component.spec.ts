import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetFormComponent } from './pet-form.component';

describe('PetFormComponent', () => {
  let component: PetFormComponent;
  let fixture: ComponentFixture<PetFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetFormComponent]
    });
    fixture = TestBed.createComponent(PetFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
