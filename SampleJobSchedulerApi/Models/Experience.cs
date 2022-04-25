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
                InMemoryDatabaseRest.Technologies.TryGetValue(TechnologyId, out Technology technology);
                if(technology != null)
                {
                    return technology.Name;
                }
                return "<Not Found>";
            }
        }
        public int YearsOfExperience { get; set; }

        public Experience(ServiceReference1.Skill e)
        {
            TechnologyId = e.TechnologyId;
            YearsOfExperience = e.YearsOfExperience;
        }
    }
}
