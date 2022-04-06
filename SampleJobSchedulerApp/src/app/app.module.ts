import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants.component';
import { SearchApplicantsComponent } from './search-applicants/search-applicants.component';
import { RecruitmentService } from './services/recruitment-service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module/material.module';
import { RejectedApplicantsComponent } from './rejected-applicants/rejected-applicants.component';

@NgModule({
  declarations: [
    AppComponent,
    SelectedApplicantsComponent,
    SearchApplicantsComponent,
    RejectedApplicantsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [RecruitmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
