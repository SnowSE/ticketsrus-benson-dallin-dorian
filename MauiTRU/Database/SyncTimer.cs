using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTRU.Database
{
    public sealed class SyncTimer
    {
        private TimeSpan timer = TimeSpan.FromSeconds(30);
        private LocalTRUDatabase? database;
        private bool cancellationflag = true;
        private bool activeflag = false;
        public TimeSpan TimerSteward {
            get; set;
        }

        public async void SetDatabase(LocalTRUDatabase db)
        {
            database = db;
            await StartThread();
        }

        public async Task StartThread()
        {
            if (!cancellationflag) {
                cancellationflag = true;
                await SyncThread();
            }
        }

        public void StopThread()
        {
            cancellationflag = false;
        }

        public async Task SyncThread()
        {
            while (cancellationflag)
            {
                activeflag = true;
                Thread.Sleep(timer);
                if(database is not null)
                { 
                    await database.UpdateLocalDbFromMainDb();
                    await database.UpdateMainDbFromLocalDb();
                }
            }
        }
    }
}
