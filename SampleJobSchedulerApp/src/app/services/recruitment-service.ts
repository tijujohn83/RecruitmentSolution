import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, of } from "rxjs";
import { Candidate, Experience } from "../models/model";
import { catchError, map } from 'rxjs/operators';

@Injectable()
export class RecruitmentService {
    private acceptedUrl: string;
    private rejectedUrl: string;
    private technologiesUrl: string;
    private searchUrl: string;
    private applicationStatusSetUrl: string;

    constructor(private http: HttpClient) {
        this.acceptedUrl = 'https://localhost:5001/api/Recruitment/accepted';
        this.rejectedUrl = 'https://localhost:5001/api/Recruitment/rejected';
        this.technologiesUrl = 'https://localhost:5001/api/Recruitment/technologies';
        this.searchUrl = 'https://localhost:5001/api/Recruitment/search';
        this.applicationStatusSetUrl = 'https://localhost:5001/api/Recruitment/application/status';
    }


    getAcceptedApplicants(): Observable<Candidate[]> {
        return this.http.get(this.acceptedUrl)
            .pipe(map((response: any) => {
                return this.mapToCandidateList(response);
            }),
                catchError((_err) => {
                    return of([]);
                }));
    }

    mapToCandidateList(candidate: any[]): Candidate[] {
        return candidate.map(c => {
            var candidate: Candidate = {
                CandidateId: c.CandidateId,
                FullName: c.FullName,
                FirstName: c.FirstName,
                LastName: c.LastName,
                Gender: c.Gender,
                ProfilePicture: c.ProfilePicture,
                Email: c.Email,
                FavoriteMusicGenre: c.FavoriteMusicGenre,
                Dad: c.Dad,
                Mom: c.Mom,
                CanSwim: c.CanSwim,
                Barcode: c.Barcode,
                Experience: this.mapToExprience(c.Experience),
                Status: c.Status
            };
            return candidate;
        });
    }

    mapToExprience(experience: any[]): Experience[] {
        return experience.map(e => {
            var experience: Experience = {
                TechnologyId: e.TechnologyId,
                TechnologyName: e.TechnologyName,
                YearsOfExperience: e.YearsOfExperience
            }
            return experience;
        });
    }
}