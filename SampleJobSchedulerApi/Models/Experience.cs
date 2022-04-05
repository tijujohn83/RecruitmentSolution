using RecruitmentApi.Models;

namespace JobApi.Models
{
    public class Experience
    {
        public string TechnologyId { get; set; }
        public string TechnologyName
        {
            get
            {
                return InMemoryDatabase.Technologies[TechnologyId].Name;
            }
        }
        public int YearsOfExperience { get; set; }
    }
}
