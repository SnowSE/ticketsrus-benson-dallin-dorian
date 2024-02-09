using LibraryTRU.IServices;
using MauiTRU.Database;
using MauiTRU.Services;
using Microsoft.Extensions.Logging;

namespace MauiTRU
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new("https://localhost:7288"); //Needs different URI for production - azure or testing
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<TRUDatabase>();
            builder.Services.AddSingleton<IDbPath, MauiDbPath>();
            builder.Services.AddSingleton<ITicketService, MauiTicketService>();
            builder.Services.AddSingleton<IConcertService, MauiConcertService>();
            builder.Services.AddSingleton(client);

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
