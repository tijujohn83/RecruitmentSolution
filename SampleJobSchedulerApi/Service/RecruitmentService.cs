using JobApi.Models;
using RecruitmentApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentApi.Service
{
    public class RecruitmentService : IRecruitmentService
    {
        public IEnumerable<Candidate> GetAcceptedCandidates()
        {
            return InMemoryDatabaseV2.GetCandidates()
                .Where(c => c.Status == ApplicationStatus.Selected)
                .ToList();
        }

        public IEnumerable<Candidate> GetRejectedCandidates()
        {
            return InMemoryDatabaseV2.GetCandidates()
              .Where(c => c.Status == ApplicationStatus.Rejected)
              .ToList();
        }

        public IEnumerable<Technology> GetTechnologies()
        {
            return InMemoryDatabaseV2.GetTechnologies();
        }

        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria)
        {
            searchCriteria.Experiences = searchCriteria.Experiences.Where(e => e.YearsOfExperience > 0);

            return InMemoryDatabaseV2.GetCandidates()
                .Where(c =>
                {
                    return c.Status == ApplicationStatus.Open
                    && searchCriteria.Experiences.Aggregate(true, (eligible, currentCriteria) =>
                    {
                        return eligible && c.Experience.Any(e => e.TechnologyId == currentCriteria.TechnologyId && e.YearsOfExperience >= currentCriteria.YearsOfExperience);
                    });
                })
                .ToList();
        }

        public UpdateStatusResult UpdateStatus(CandidateStatus candidateStatus)
        {
            InMemoryDatabaseV2.Candidates.TryGetValue(candidateStatus.CandidateId, out Candidate candidate);
            if (candidate == null)
            {
                return UpdateStatusResult.NotFound;
            }
            if (candidate.Status == ApplicationStatus.Open)
            {
                candidate.Status = candidateStatus.Status;
                return UpdateStatusResult.Success;
            }
            return UpdateStatusResult.Failure;
        }

        public void Reset()
        {
            InMemoryDatabaseV2.Reset();
        }

    }
}
