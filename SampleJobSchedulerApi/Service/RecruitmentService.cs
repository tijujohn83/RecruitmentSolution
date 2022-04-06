﻿using JobApi.Models;
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
                .Where(c => c.Status == ApplicationStatus.Selected)
                .ToList();
        }

        public IEnumerable<Candidate> GetRejectedCandidates()
        {
            return InMemoryDatabase.GetCandidates()
              .Where(c => c.Status == ApplicationStatus.Rejected)
              .ToList();
        }

        public IEnumerable<Technology> GetTechnologies()
        {
            return InMemoryDatabase.GetTechnologies();
        }

        public IEnumerable<Candidate> SearchCandidates(SearchCriteria searchCriteria)
        {
            return InMemoryDatabase.GetCandidates()
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
            InMemoryDatabase.Candidates.TryGetValue(candidateStatus.CandidateId, out Candidate candidate);
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


    }
}
