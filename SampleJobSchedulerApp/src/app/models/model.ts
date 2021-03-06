export class Experience {
    technologyId: string;
    technologyName: string;
    yearsOfExperience: number;

    constructor(partial?: Partial<Experience>) {
        Object.assign(this, partial);
    }
}

export class Candidate {
    candidateId: string;
    fullName: string;
    firstName: string;
    lastName: string;
    gender: number;
    profilePicture: string;
    email: string;
    favoriteMusicGenre: string;
    dad: string;
    mom: string;
    canSwim: boolean;
    barcode: string;
    experience: Experience[];
    status: string;

    get experienceSummary(): string {
        return this.experience.map(e => {
            return e.technologyName + ' (' + e.yearsOfExperience + 'y)';
        }).join(', ');
    }    

    constructor(partial?: Partial<Candidate>) {
        Object.assign(this, partial);
    }
}

export class Technology {
    name: string;
    guid: string;

    constructor(partial?: Partial<Technology>) {
        Object.assign(this, partial);
    }
}

export class SearchCriteria {
    experience: Experience[];
}

export enum ApplicationStatus {
    Open = 'Open',
    Selected = 'Selected',
    Rejected = 'Rejected'
}

export enum UpdateStatusResult {
    NotFound = 'NotFound',
    Success = 'Success',
    Failure = 'Failure'
}

export enum ResetResult {
    Success = 'Success',
    Failure = 'Failure'
}

export class CandidateStatus {
    candidateId: string;
    status:ApplicationStatus;
}