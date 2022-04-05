using JobApi.Models;
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
        private readonly SourceOptions _sourceOptions;


        public SyncService(ILogger<SyncService> logger, IDataSeed dataSeed, IOptions<SourceOptions> sourceOptions)
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
            var candidates = InMemoryDatabase.GetCandidates();
            if (candidates.Any())
            {
                File.WriteAllTextAsync(_sourceOptions.CandidatesFile, JsonSerializer.Serialize(InMemoryDatabase.GetCandidates()));
            }

            var technologies = InMemoryDatabase.GetTechnologies();
            if (technologies.Any())
            {
                File.WriteAllTextAsync(_sourceOptions.TechnologiesFile, JsonSerializer.Serialize(InMemoryDatabase.GetTechnologies()));
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
