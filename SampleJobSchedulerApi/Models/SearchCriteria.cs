using JobApi.Models;
using System.Collections.Generic;

namespace RecruitmentApi.Models
{
    public class SearchCriteria
    {
        public IEnumerable<Experience> Experiences { get; set; }
    }
}
