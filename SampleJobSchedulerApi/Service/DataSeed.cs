using JobApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecruitmentApi.Service
{
    public class DataSeed : IDataSeed
    {
        private readonly ILogger<DataSeed> _logger;
        private ConfigOptions _sourceOptions;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

    public DataSeed(IOptions<ConfigOptions> sourceOptions, ILogger<DataSeed> logger)
        {
            _sourceOptions = sourceOptions.Value;
            _logger = logger;
        }

        public async Task Seed()
        {
            IEnumerable<Technology> technologies;
            IEnumerable<Candidate> candidates;

            try
            {

                if (File.Exists(_sourceOptions.TechnologiesFile))
                {
                    technologies = JsonSerializer.Deserialize<IEnumerable<Technology>>(await File.ReadAllTextAsync(_sourceOptions.TechnologiesFile), _jsonSerializerOptions);
                    InMemoryDatabase.AddTechnologies(technologies);

                } else
                {
                    var client = new HttpClient();
                    technologies = await JsonSerializer.DeserializeAsync<IEnumerable<Technology>>(await client.GetStreamAsync(_sourceOptions.TechnologiesUrl), _jsonSerializerOptions);
                    InMemoryDatabase.AddTechnologies(technologies);
                }
                if (File.Exists(_sourceOptions.CandidatesFile))
                {
                    candidates = JsonSerializer.Deserialize<IEnumerable<Candidate>>(await File.ReadAllTextAsync(_sourceOptions.CandidatesFile), _jsonSerializerOptions);
                    InMemoryDatabase.AddTechnologies(technologies);

                } else
                {
                    var client = new HttpClient();
                    candidates = await JsonSerializer.DeserializeAsync<IEnumerable<Candidate>>(await client.GetStreamAsync(_sourceOptions.CandidatesUrl), _jsonSerializerOptions);
                    InMemoryDatabase.AddTechnologies(technologies);
                }

                InMemoryDatabase.AddTechnologies(technologies);
                InMemoryDatabase.AddCandidates(candidates);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }

    }
}
