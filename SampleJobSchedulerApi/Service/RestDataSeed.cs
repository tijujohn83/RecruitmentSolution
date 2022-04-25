using JobApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecruitmentApi.Service
{
    public class RestDataSeed : IDataSeed
    {
        private readonly ILogger<RestDataSeed> _logger;
        private ConfigOptions _sourceOptions;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

    public RestDataSeed(IOptions<ConfigOptions> sourceOptions, ILogger<RestDataSeed> logger)
        {
            _sourceOptions = sourceOptions.Value;
            _logger = logger;
        }

        public async Task Seed(bool reset = false)
        {
            IEnumerable<Technology> technologies;
            IEnumerable<Candidate> candidates;

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
                    technologies = JsonSerializer.Deserialize<IEnumerable<Technology>>(await File.ReadAllTextAsync(_sourceOptions.TechnologiesFile), _jsonSerializerOptions);

                } else
                {
                    var client = new HttpClient();
                    technologies = await JsonSerializer.DeserializeAsync<IEnumerable<Technology>>(await client.GetStreamAsync(_sourceOptions.TechnologiesUrl), _jsonSerializerOptions);
                }
                if (File.Exists(_sourceOptions.CandidatesFile))
                {
                    candidates = JsonSerializer.Deserialize<IEnumerable<Candidate>>(await File.ReadAllTextAsync(_sourceOptions.CandidatesFile), _jsonSerializerOptions);
                } else
                {
                    var client = new HttpClient();
                    candidates = await JsonSerializer.DeserializeAsync<IEnumerable<Candidate>>(await client.GetStreamAsync(_sourceOptions.CandidatesUrl), _jsonSerializerOptions);
                }

                InMemoryDatabaseRest.AddTechnologies(technologies);
                InMemoryDatabaseRest.AddCandidates(candidates);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }

    }
}
