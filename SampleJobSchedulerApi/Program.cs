using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RecruitmentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);  
            builder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            host.ConfigureLogging((logging) => 
            {
                logging.ClearProviders();
                logging.AddJsonConsole();
            });
            host.ConfigureWebHostDefaults(webBuilder => 
            {
                webBuilder.UseStartup<Startup>();                 
            });
            return host;
        }

    }
}
