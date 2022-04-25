namespace JobApi.Models
{
    public class Technology : ITechnology
    {
        public string Name { get; set; }
        public string Guid { get; set; }

        public Technology(ServiceReference1.Technology tech)
        {
            Name = tech.Name;
            Guid = tech.Guid;
        }
    }


}
