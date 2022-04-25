using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentApi.Models;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecruitmentApi.Service
{
    public class SoapDataSeed : IDataSeed
    {
        private readonly ILogger<RestDataSeed> _logger;
        private ConfigOptions _sourceOptions;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

    public SoapDataSeed(IOptions<ConfigOptions> sourceOptions, ILogger<RestDataSeed> logger)
        {
            _sourceOptions = sourceOptions.Value;
            _logger = logger;
        }

        public async Task Seed(bool reset = false)
        {
            IEnumerable<JobApi.Models.Technology> technologies;
            IEnumerable<JobApi.Models.Candidate> candidates;

            if (reset)
            {
                if (File.Exists(_sourceOptions.TechnologiesFile))
                    File.Delete(_sourceOptions.TechnologiesFile);

                if (File.Exists(_sourceOptions.CandidatesFile))
                    File.Delete(_sourceOptions.CandidatesFile);
            }

            try
            {
                if (File.Exists(_sourceOptions.TechnologiesFile))
                {
                    technologies = JsonSerializer.Deserialize<IEnumerable<JobApi.Models.Technology>>(await File.ReadAllTextAsync(_sourceOptions.TechnologiesFile), _jsonSerializerOptions);

                } else
                {
                    var soapClient = new RecruitmentServiceClient();
                    var techs = await soapClient.GetTechnologiesAsync();
                    technologies = techs.ToList().Select(t => new JobApi.Models.Technology(t));
                }
                if (File.Exists(_sourceOptions.CandidatesFile))
                {
                    candidates = JsonSerializer.Deserialize<IEnumerable<JobApi.Models.Candidate>>(await File.ReadAllTextAsync(_sourceOptions.CandidatesFile), _jsonSerializerOptions);                    

                } else
                {
                    var soapClient = new RecruitmentServiceClient();
                    var clients = await soapClient.GetCandidatesAsync();
                    candidates = clients.Select(c => new JobApi.Models.Candidate(c));                    
                }

                InMemoryDatabaseSoap.AddTechnologies(technologies);
                InMemoryDatabaseSoap.AddCandidates(candidates);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }

    }
}
