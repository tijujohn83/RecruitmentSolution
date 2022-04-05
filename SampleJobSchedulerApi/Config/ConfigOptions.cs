using System.Collections.Generic;

namespace RecruitmentApi.Models
{
    public class ConfigOptions
    {
        public string TechnologiesUrl { get; set; }
        public string CandidatesUrl { get; set; }
        public string TechnologiesFile { get; set; }
        public string CandidatesFile { get; set; }
        public IEnumerable<string> AllowedOrigins { get; set; }
    }
}
