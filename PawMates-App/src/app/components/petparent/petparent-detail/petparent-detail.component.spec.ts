import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetparentDetailComponent } from './petparent-detail.component';

describe('PetparentDetailComponent', () => {
  let component: PetparentDetailComponent;
  let fixture: ComponentFixture<PetparentDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PetparentDetailComponent]
    });
    fixture = TestBed.createComponent(PetparentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
