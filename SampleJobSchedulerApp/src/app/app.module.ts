import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SelectedApplicantsComponent } from './selected-applicants/selected-applicants.component';
import { SearchApplicantsComponent } from './search-applicants/search-applicants.component';
import { RecruitmentService } from './services/recruitment-service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    SelectedApplicantsComponent,
    SearchApplicantsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [RecruitmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
