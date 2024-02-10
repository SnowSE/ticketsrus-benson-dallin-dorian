using LibraryTRU.Data;
using LibraryTRU.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApiTRU.Services;

namespace TestsTRU
{
    public class TRUWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;
        public TRUWebAppFactory()
        {
            var backupFile = Directory.GetFiles("../../..", "*.sql", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.LastWriteTime)
                .First();

            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres")
                .WithPassword("P@ssword1")
                .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => {
                services.RemoveAll(typeof(DbContextOptions<PostgresContext>));
                services.RemoveAll(typeof(PostgresContext));

                /*var dbc = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(DbContextOptions<PostgresContext>));

                var pg = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(PostgresContext));

                var db = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(DbContext));*/

                services.AddDbContext<PostgresContext>(o =>
                {
                    o.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.RemoveAll(typeof(ITicketService));
                services.RemoveAll(typeof(IConcertService));

                /*var ts = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(TicketService));

                var cs = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(ConcertService));*/

                services.AddScoped<ITicketService, TicketService>();
                services.AddScoped<IConcertService, ConcertService>();

                /*ts = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(ITicketService));

                cs = services.SingleOrDefault(s =>
                        s.ServiceType == typeof(IConcertService));*/
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
