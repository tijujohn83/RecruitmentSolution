import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedApplicantsComponent } from './selected-applicants.component';

describe('SelectedApplicantsComponent', () => {
  let component: SelectedApplicantsComponent;
  let fixture: ComponentFixture<SelectedApplicantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectedApplicantsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectedApplicantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
