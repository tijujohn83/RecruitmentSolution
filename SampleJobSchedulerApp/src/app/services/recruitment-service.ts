import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, of } from "rxjs";
import { Candidate, CandidateStatus, Experience, ResetResult, Technology, UpdateStatusResult } from "../models/model";
import { catchError, map } from 'rxjs/operators';

@Injectable()
export class RecruitmentService {
    private selectedUrl: string;
    private rejectedUrl: string;
    private technologiesUrl: string;
    private searchUrl: string;
    private applicationStatusSetUrl: string;
    private resetUrl: string;

    constructor(private http: HttpClient) {
        this.selectedUrl = 'https://localhost:5001/api/Recruitment/selected';
        this.rejectedUrl = 'https://localhost:5001/api/Recruitment/rejected';
        this.technologiesUrl = 'https://localhost:5001/api/Recruitment/technologies';
        this.searchUrl = 'https://localhost:5001/api/Recruitment/search';
        this.applicationStatusSetUrl = 'https://localhost:5001/api/Recruitment/application/status';
        this.resetUrl = "https://localhost:5001/api/Recruitment/reset";
    }


    getAcceptedApplicants(): Observable<Candidate[]> {
        return this.http.get(this.selectedUrl)
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

    reset(): Observable<ResetResult> {
        return this.http.get(this.resetUrl)
            .pipe(map((response: any) => {
                return this.mapToResetResult(response);
            }),
                catchError((_err) => {
                    return of(ResetResult.Failure);
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

    setApplicationStatus(candidateStatus: CandidateStatus): Observable<UpdateStatusResult> {
        return this.http.post(this.applicationStatusSetUrl, candidateStatus)
            .pipe(map((response: any) => {
                return this.mapToUpdateStatusResult(response);
            }),
                catchError((_err) => {
                    return of(UpdateStatusResult.Failure);
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

    mapToUpdateStatusResult(result: any): UpdateStatusResult {
        return result as UpdateStatusResult;
    }

    mapToResetResult(result: any): ResetResult {
        return result as ResetResult;
    }
}