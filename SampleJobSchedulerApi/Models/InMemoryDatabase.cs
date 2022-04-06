using JobApi.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentApi.Models
{
    public static class InMemoryDatabase
    {
        public static object _lockObj = new object();
        public static ConcurrentDictionary<string, Candidate> Candidates { get; } = new ConcurrentDictionary<string, Candidate>();
        public static ConcurrentDictionary<string, Technology> Technologies { get; } = new ConcurrentDictionary<string, Technology>();

        public static void AddCandidates(IEnumerable<Candidate> candidates)
        {
            candidates.ToList().ForEach(candidate => Candidates.TryAdd(candidate.CandidateId,  candidate));
        }

        public static void AddTechnologies(IEnumerable<Technology> technologies)
        {
            technologies.ToList().ForEach(technology => Technologies.TryAdd(technology.Guid, technology));
        }

        public static IEnumerable<Candidate> GetCandidates()
        {
            return Candidates.Values;
        }

        public static IEnumerable<Technology> GetTechnologies()
        {
            return Technologies.Values;
        }

        public static void Reset()
        {
            lock (_lockObj)
            {
                Candidates.Clear();
                Technologies.Clear();
            }
        }
    }
}
