import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Candidate } from '../models/model';
import { RecruitmentService } from '../services/recruitment-service';

@Component({
  selector: 'app-rejected-applicants',
  templateUrl: './rejected-applicants.component.html',
  styleUrls: ['./rejected-applicants.component.css']
})
export class RejectedApplicantsComponent implements OnInit {

  dataSource:MatTableDataSource<Candidate> = new MatTableDataSource<Candidate>();
  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'gender',
    'email',    
    'status',
    'experienceSummary',
  ];
  constructor(private recruitmentService:RecruitmentService) { 
  }

  ngOnInit(): void {
    this.recruitmentService.getRejectedApplicants().subscribe((applicants) => {
      this.dataSource.data = applicants;
    })
  }

  showAccepted() {
    
  }

}
