using System.Collections.Generic;
using System.Linq;

namespace JobApi.Models
{
    public class Candidate : ICandidate
    {
        public string CandidateId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string FavoriteMusicGenre { get; set; }
        public string Dad { get; set; }
        public string Mom { get; set; }
        public bool CanSwim { get; set; }
        public string Barcode { get; set; }
        public IEnumerable<Experience> Experience { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Open;


        public Candidate(ServiceReference1.Candidate candidate)
        {
            CandidateId = candidate.CandidateId;
            FullName = candidate.FullName;
            FirstName = candidate.FirstName;
            LastName = candidate.LastName;
            Gender = (int)candidate.Gender;
            ProfilePicture = candidate.ProfilePicture;
            Email = candidate.Email;
            FavoriteMusicGenre = candidate.FavoriteMusicGenre;
            Dad = candidate.Dad;
            Mom = candidate.Mom;
            CanSwim = candidate.CanSwim;
            Barcode = candidate.Barcode;
            Experience = candidate.Experience.ToList().Select(e => new Models.Experience(e));
            Status = ApplicationStatus.Open;                 
        }
    }

}
