import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchApplicantsComponent } from './search-applicants.component';

describe('SearchApplicantsComponent', () => {
  let component: SearchApplicantsComponent;
  let fixture: ComponentFixture<SearchApplicantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchApplicantsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchApplicantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
