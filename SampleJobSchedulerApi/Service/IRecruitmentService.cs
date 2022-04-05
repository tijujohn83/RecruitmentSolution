using JobApi.Models;
using RecruitmentApi.Models;
using System.Collections.Generic;

namespace RecruitmentApi.Service
{
    public interface IRecruitmentService
    {
        public IEnumerable<Candidate> GetAcceptedCandidates();
        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria);
        public CandidateStatus UpdateStatus(CandidateStatus candidateStatus);
    }
}
