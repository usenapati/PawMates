import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaydatesPlaydateComponent } from './playdates-playdate.component';

describe('PlaydatesPlaydateComponent', () => {
  let component: PlaydatesPlaydateComponent;
  let fixture: ComponentFixture<PlaydatesPlaydateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaydatesPlaydateComponent]
    });
    fixture = TestBed.createComponent(PlaydatesPlaydateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
