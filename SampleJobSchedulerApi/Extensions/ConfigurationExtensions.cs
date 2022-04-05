using Microsoft.Extensions.Configuration;

namespace RecruitmentApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration) where T : new()
        {
            T settings = new T();
            var sectionName = typeof(T).Name;
            configuration.GetSection(sectionName).Bind(settings);
            return settings;
        }
    }
}
