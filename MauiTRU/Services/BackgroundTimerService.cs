using MauiTRU.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTRU.Services
{
    public class BackgroundTimerService : BackgroundService
    {
        private readonly ILogger<BackgroundTimerService> _logger;
        private readonly LocalTRUDatabase _db;
        private int _timeperiod = 30;
        private int _execCount;
        
        public BackgroundTimerService(ILogger<BackgroundTimerService> logger, LocalTRUDatabase database)
        {
            _logger = logger;
            _db = database;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting timer");
            
            await DoWork();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(_timeperiod));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWork();
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation("Timer is stopping");
            }
        }

        private async Task DoWork()
        {
            _logger.LogInformation("Synchronizing databases...");

            if (_db is not null && Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await _db.UpdateLocalDbFromMainDb();
                await _db.UpdateMainDbFromLocalDb();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        public async Task RestartTimer()
        {
            await StopAsync(new CancellationToken());
            await ExecuteAsync(new CancellationToken());
        }

        public async Task ChangeTimePeriod(int periodinseconds)
        {
            _timeperiod = periodinseconds;
            await RestartTimer();
        }
    }
}
