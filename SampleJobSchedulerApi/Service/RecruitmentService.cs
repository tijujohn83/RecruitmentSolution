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
            return InMemoryDatabase.GetCandidates()
                .Where(c => c.Status == ApplicationStatus.Accepted)
                .ToList();
        }

        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria)
        {
            return InMemoryDatabase.GetCandidates()
                .Where(c => !searchCriteria.Technologies.Except(c.Experiences.Select(t => t.TechnologyId)).Any())
                .Where(c => c.Ex searchCriteria)
                .ToList();
        }

        public CandidateStatus UpdateStatus(CandidateStatus candidateStatus)
        {
            InMemoryDatabase.Candidates[candidateStatus.Candidate.CandidateId].Status = candidateStatus.Status;
            return new CandidateStatus { 
                Candidate = InMemoryDatabase.Candidates[candidateStatus.Candidate.CandidateId], 
                Status = InMemoryDatabase.Candidates[candidateStatus.Candidate.CandidateId].Status 
            };
        }
    }
}
