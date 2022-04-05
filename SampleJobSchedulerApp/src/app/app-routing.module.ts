import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchApplicantsComponent } from './search-applicants/search-applicants.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants.component';

const routes: Routes = 
[
  {
    path: 'search',
    component: SearchApplicantsComponent,
  },
  {
    path: 'list',
    component: SelectedApplicantsComponent
  },
  {
    path: '**',
    redirectTo: 'list'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
