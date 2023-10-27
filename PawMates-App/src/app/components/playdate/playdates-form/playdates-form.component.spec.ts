import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaydatesFormComponent } from './playdates-form.component';

describe('PlaydatesFormComponent', () => {
  let component: PlaydatesFormComponent;
  let fixture: ComponentFixture<PlaydatesFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaydatesFormComponent]
    });
    fixture = TestBed.createComponent(PlaydatesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
