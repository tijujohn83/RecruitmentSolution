import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RejectedApplicantsComponent } from './rejected-applicants.component';

describe('RejectedApplicantsComponent', () => {
  let component: RejectedApplicantsComponent;
  let fixture: ComponentFixture<RejectedApplicantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RejectedApplicantsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RejectedApplicantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
