import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RejectedApplicantsComponent } from './rejected-applicants/rejected-applicants.component';
import { SearchApplicantsComponent } from './search-applicants/search-applicants.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants.component';

const routes: Routes = 
[
  {
    path: 'search',
    component: SearchApplicantsComponent,
  },
  {
    path: 'selected',
    component: SelectedApplicantsComponent
  },
  {
    path: 'rejected',
    component: RejectedApplicantsComponent
  },
  {
    path: '**',
    redirectTo: 'search'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
