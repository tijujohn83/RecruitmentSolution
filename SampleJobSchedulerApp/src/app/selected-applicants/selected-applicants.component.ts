import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Candidate } from '../models/model';
import { RecruitmentService } from '../services/recruitment-service';

@Component({
  selector: 'app-selected-applicants',
  templateUrl: './selected-applicants.component.html',
  styleUrls: ['./selected-applicants.component.css']
})
export class SelectedApplicantsComponent implements OnInit {

  dataSource:MatTableDataSource<Candidate> = new MatTableDataSource<Candidate>();
  displayedColumns: string[] = [
    '#',
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
    this.recruitmentService.getAcceptedApplicants().subscribe((applicants) => {
      this.dataSource.data = applicants;
    })
  }

}
