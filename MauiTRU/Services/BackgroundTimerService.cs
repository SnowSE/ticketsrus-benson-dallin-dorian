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
    public class BackgroundTimerService
    {
        private readonly LocalTRUDatabase _db;
        private int _timeperiod = Constants.DefaultRefreshRate;
        public bool isRunning;
        
        public BackgroundTimerService(LocalTRUDatabase database)
        {
            _db = database;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting timer");
            
            await DoWork();
            isRunning = true;

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
                Console.WriteLine("Timer is stopping");
            }
        }

        private async Task DoWork()
        {
            Console.WriteLine("Synchronizing databases...");

            if (_db is not null && Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await _db.UpdateLocalDbFromMainDb();
                await _db.UpdateMainDbFromLocalDb();
            }
        }

        public async Task RestartTimer()
        {
            await ExecuteAsync(new CancellationToken(true));
            await ExecuteAsync(new CancellationToken());
        }

        public async Task ChangeTimePeriod(int periodinseconds)
        {
            _timeperiod = periodinseconds;
            await RestartTimer();
        }
    }
}
