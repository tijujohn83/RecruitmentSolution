import { Component, OnInit } from '@angular/core';
import { RecruitmentService } from '../services/recruitment-service';

@Component({
  selector: 'app-selected-applicants',
  templateUrl: './selected-applicants.component.html',
  styleUrls: ['./selected-applicants.component.css']
})
export class SelectedApplicantsComponent implements OnInit {

  constructor(private recruitmentService:RecruitmentService) { 

  }

  ngOnInit(): void {
    this.recruitmentService.getAcceptedApplicants().subscribe((applicants) => {
      console.dir(applicants)
    })
  }

}
