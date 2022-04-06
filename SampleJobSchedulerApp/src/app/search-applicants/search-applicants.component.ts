import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { pipe } from 'rxjs';
import { Candidate, Technology } from '../models/model';
import { RecruitmentService } from '../services/recruitment-service';

@Component({
  selector: 'app-search-applicants',
  templateUrl: './search-applicants.component.html',
  styleUrls: ['./search-applicants.component.css']
})
export class SearchApplicantsComponent implements OnInit {

  candidateDataSource:MatTableDataSource<Candidate> = new MatTableDataSource<Candidate>();
  technologiesDataSource:MatTableDataSource<Technology> = new MatTableDataSource<Technology>();

  technologiesColumns: string[] = [
    'name'
  ];
  candidateColumns: string[] = [
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
    this.recruitmentService.getTechnologies()
    .pipe()
    .subscribe((applicants) => {
      this.technologiesDataSource.data = applicants;
    })
  }

  search():void {
    this.recruitmentService.searchApplicants([])
    .pipe()
    .subscribe((applicants) => {
      this.candidateDataSource.data = applicants;
    })
  }

}
