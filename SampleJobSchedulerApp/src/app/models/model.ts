export interface Experience {
    TechnologyId: string;
    TechnologyName: string;
    YearsOfExperience: number;
}

export interface Candidate {
    CandidateId: string;
    FullName: string;
    FirstName: string;
    LastName: string;
    Gender: number;
    ProfilePicture: string;
    Email: string;
    FavoriteMusicGenre: string;
    Dad: string;
    Mom: string;
    CanSwim: boolean;
    Barcode: string;
    Experience: Experience[];
    Status: string;
}

export interface Technology {
    Name: string;
    Guid: string;
}