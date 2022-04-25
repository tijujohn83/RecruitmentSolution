using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecruitmentApi.Middleware;
using RecruitmentApi.Models;
using RecruitmentApi.Service;
using System.Net;
using RecruitmentApi.Extensions;
using System.Linq;

namespace RecruitmentApi
{
    public class Startup
    {
        private readonly string _corsPolicy = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigOptions>(Configuration.GetSection("ConfigOptions"));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecruitmentApi", Version = "v1" });
            });

            var healthChecksBuilder = services.AddHealthChecks();

            var configOptions = Configuration.GetSection<ConfigOptions>();

            services.AddCors(o => o.AddPolicy(_corsPolicy, builder =>
            {
                builder.SetIsOriginAllowed(host => configOptions.AllowedOrigins.Any(origin => origin.TrimEnd(new[] { '/' }).Equals(host.TrimEnd(new[] { '/' }))))
                    .WithOrigins(configOptions.AllowedOrigins.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services
            .AddSingleton<IDataSeed, SoapDataSeed>()
            .AddTransient<IRecruitmentService, RecruitmentService>()
            .AddHostedService<SyncService>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecruitmentApi v1"));
            }

            app.UseCors(_corsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false
            });

        }
    }
}
