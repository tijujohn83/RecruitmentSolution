import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { delay } from 'rxjs/operators';
import { ApplicationStatus, Candidate, CandidateStatus, Experience, ResetResult, Technology } from '../models/model';
import { RecruitmentService } from '../services/recruitment-service';

@Component({
  selector: 'app-search-applicants',
  templateUrl: './search-applicants.component.html',
  styleUrls: ['./search-applicants.component.css']
})
export class SearchApplicantsComponent implements OnInit {

  candidateDataSource: MatTableDataSource<Candidate> = new MatTableDataSource<Candidate>();
  technologiesDataSource: MatTableDataSource<Technology> = new MatTableDataSource<Technology>();

  technologiesColumns: string[] = [
    'name',
    'actions'
  ];
  candidateColumns: string[] = [
    '#',
    'firstName',
    'lastName',
    'gender',
    'email',
    'status',
    'experienceSummary',
    'actions'
  ];

  experienceFilter: { technologyId: string, yearsOfExperience: number, selected: boolean }[] = [];

  constructor(private recruitmentService: RecruitmentService) {
  }

  ngOnInit(): void {
    this.recruitmentService.getTechnologies()
      .pipe()
      .subscribe((technologies) => {
        this.technologiesDataSource.data = technologies;
        this.experienceFilter = technologies.map(tech => {
          return { technologyId: tech.guid, yearsOfExperience: 0, selected: false };
        })
      });

    this.search();
  }

  search(): void {
    this.recruitmentService.searchApplicants(this.experienceFilter.filter(exp => exp.selected)
      .map(exp => <Experience>{ technologyId: exp.technologyId, yearsOfExperience: exp.yearsOfExperience }))
      .pipe()
      .subscribe((candidates) => {
        this.candidateDataSource.data = candidates;
      })
  }

  select(candidate: Candidate) {
    this.recruitmentService.setApplicationStatus(<CandidateStatus>{ candidateId: candidate.candidateId, status: ApplicationStatus.Selected })
      .pipe()
      .subscribe(result => {
        if (result) {
          this.search();
        }
      })
  }

  reject(candidate: Candidate) {
    this.recruitmentService.setApplicationStatus(<CandidateStatus>{ candidateId: candidate.candidateId, status: ApplicationStatus.Rejected })
      .pipe()
      .subscribe(result => {
        if (result) {
          this.search();
        }
      })
  }

  technologySelectionChanged(event: any, technology: Technology): void {
    var exp = this.experienceFilter.find(exp => exp.technologyId === technology.guid);
    if (exp != undefined) {
      exp.selected = event.checked;
    }

  }

  yearsChanged(event: any, technology: Technology): void {
    var exp = this.experienceFilter.find(exp => exp.technologyId === technology.guid);
    if (exp != undefined) {
      exp.yearsOfExperience = event.srcElement.value;
    }
  }

  resetServerData(): void {
    this.recruitmentService.reset()
      .pipe(delay(3000))
      .subscribe(() => {
        this.search();
      });
  }

}
