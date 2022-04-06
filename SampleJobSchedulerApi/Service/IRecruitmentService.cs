using JobApi.Models;
using RecruitmentApi.Models;
using System.Collections.Generic;

namespace RecruitmentApi.Service
{
    public interface IRecruitmentService
    {
        public IEnumerable<Candidate> GetAcceptedCandidates();
        public IEnumerable<Candidate> GetRejectedCandidates();
        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria);
        public UpdateStatusResult UpdateStatus(CandidateStatus candidateStatus);
        public IEnumerable<Technology> GetTechnologies();
        public void Reset();
    }
}
