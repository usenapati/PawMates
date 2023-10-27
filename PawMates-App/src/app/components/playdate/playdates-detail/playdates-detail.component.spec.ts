import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaydatesDetailComponent } from './playdates-detail.component';

describe('PlaydatesDetailComponent', () => {
  let component: PlaydatesDetailComponent;
  let fixture: ComponentFixture<PlaydatesDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaydatesDetailComponent]
    });
    fixture = TestBed.createComponent(PlaydatesDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
