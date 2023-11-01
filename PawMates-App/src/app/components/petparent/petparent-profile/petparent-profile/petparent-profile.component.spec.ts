import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetparentProfileComponent } from './petparent-profile.component';

describe('PetparentProfileComponent', () => {
  let component: PetparentProfileComponent;
  let fixture: ComponentFixture<PetparentProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetparentProfileComponent]
    });
    fixture = TestBed.createComponent(PetparentProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
