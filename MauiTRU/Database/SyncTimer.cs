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
        public TimeSpan TimerSteward {
            get; set;
        }

        public void SetDatabase(LocalTRUDatabase db)
        {
            database = db;
        }

        public async Task StartThread()
        {
            await SyncThread();
        }

        public void StopThread()
        {
            cancellationflag = false;
        }

        public async Task SyncThread()
        {
            while (cancellationflag)
            {
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
