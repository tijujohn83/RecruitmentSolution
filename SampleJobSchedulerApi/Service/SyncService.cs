using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentApi.Service
{
    public class SyncService : IHostedService, IDisposable
    {

        private CancellationToken _cancellationToken;
        private IDataSeed _dataSeed;
        private readonly ILogger<SyncService> _logger;
        private readonly ConfigOptions _sourceOptions;


        public SyncService(ILogger<SyncService> logger, IDataSeed dataSeed, IOptions<ConfigOptions> sourceOptions)
        {
            _logger = logger;
            _dataSeed = dataSeed;
            _sourceOptions = sourceOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            InitializeAndSync();
            await Task.CompletedTask;
        }

        private void InitializeAndSync()
        {
            _dataSeed.Seed();
            Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await Sync();
                    await Task.Delay(2000);
                }
            });

        }

        private Task Sync()
        {
            var technologies = InMemoryDatabaseRest.GetTechnologies();
            if (technologies.Any())
            {
                File.WriteAllTextAsync(_sourceOptions.TechnologiesFile, JsonSerializer.Serialize(InMemoryDatabaseRest.GetTechnologies()));
            } else
            {
                _dataSeed.Seed(true);
            }

            var candidates = InMemoryDatabaseRest.GetCandidates();
            if (candidates.Any())
            {
                File.WriteAllTextAsync(_sourceOptions.CandidatesFile, JsonSerializer.Serialize(InMemoryDatabaseRest.GetCandidates()));
            }

            

            return Task.FromResult(0);
        }

        public void Dispose()
        {

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
