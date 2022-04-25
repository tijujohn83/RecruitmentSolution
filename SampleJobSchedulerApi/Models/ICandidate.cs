using System.Collections.Generic;

namespace JobApi.Models
{
    public interface ICandidate
    {
        string Barcode { get; set; }
        string CandidateId { get; set; }
        bool CanSwim { get; set; }
        string Dad { get; set; }
        string Email { get; set; }
        IEnumerable<Experience> Experience { get; set; }
        string FavoriteMusicGenre { get; set; }
        string FirstName { get; set; }
        string FullName { get; set; }
        int Gender { get; set; }
        string LastName { get; set; }
        string Mom { get; set; }
        string ProfilePicture { get; set; }
        ApplicationStatus Status { get; set; }
    }
}