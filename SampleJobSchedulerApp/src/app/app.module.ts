import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants.component';
import { SearchApplicantsComponent } from './search-applicants/search-applicants.component';

@NgModule({
  declarations: [
    AppComponent,
    SelectedApplicantsComponent,
    SearchApplicantsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
