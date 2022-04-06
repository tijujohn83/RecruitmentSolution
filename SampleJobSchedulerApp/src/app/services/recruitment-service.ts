import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, of } from "rxjs";
import { Candidate, Experience, Technology } from "../models/model";
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

    getRejectedApplicants(): Observable<Candidate[]> {
        return this.http.get(this.rejectedUrl)
            .pipe(map((response: any) => {
                return this.mapToCandidateList(response);
            }),
                catchError((_err) => {
                    return of([]);
                }));
    }

    getTechnologies(): Observable<Technology[]> {
        return this.http.get(this.technologiesUrl)
            .pipe(map((response: any) => {
                return this.mapToTechnologyList(response);
            }),
                catchError((_err) => {
                    return of([]);
                }));
    }

    searchApplicants(experience: Experience[]): Observable<Candidate[]> {
        return this.http.post(this.searchUrl, { experiences: experience })
            .pipe(map((response: any) => {
                return this.mapToCandidateList(response);
            }),
                catchError((_err) => {
                    return of([]);
                }));
    }

    mapToCandidateList(candidates: any[]): Candidate[] {
        return candidates.map(c => {
            return new Candidate({
                candidateId: c.candidateId,
                fullName: c.fullName,
                firstName: c.firstName,
                lastName: c.lastName,
                gender: c.gender,
                profilePicture: c.profilePicture,
                email: c.email,
                favoriteMusicGenre: c.favoriteMusicGenre,
                dad: c.dad,
                mom: c.mom,
                canSwim: c.canSwim,
                barcode: c.barcode,
                experience: this.mapToExprience(c.experience),
                status: c.status
            });
        });
    }

    mapToExprience(experiences: any[]): Experience[] {
        return experiences.map(e => {
            return new Experience({
                technologyId: e.technologyId,
                technologyName: e.technologyName,
                yearsOfExperience: e.yearsOfExperience
            });
        });
    }

    mapToTechnologyList(technologies: any[]): Technology[] {
        return technologies.map(e => {
            return new Technology({
                name: e.name,
                guid: e.guid
            });
        });
    }
}