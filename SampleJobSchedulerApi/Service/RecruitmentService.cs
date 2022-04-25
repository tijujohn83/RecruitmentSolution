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
            return InMemoryDatabaseSoap.GetCandidates()
                .Where(c => c.Status == ApplicationStatus.Selected)
                .ToList();
        }

        public IEnumerable<Candidate> GetRejectedCandidates()
        {
            return InMemoryDatabaseSoap.GetCandidates()
              .Where(c => c.Status == ApplicationStatus.Rejected)
              .ToList();
        }

        public IEnumerable<Technology> GetTechnologies()
        {
            return InMemoryDatabaseSoap.GetTechnologies();
        }

        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria)
        {
            searchCriteria.Experiences = searchCriteria.Experiences.Where(e => e.YearsOfExperience > 0);

            return InMemoryDatabaseSoap.GetCandidates()
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
            InMemoryDatabaseSoap.Candidates.TryGetValue(candidateStatus.CandidateId, out Candidate candidate);
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
            InMemoryDatabaseSoap.Reset();
        }

    }
}
