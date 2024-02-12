using Microsoft.EntityFrameworkCore;
using WebApiTRU.Components;
using WebApiTRU.Services;

public class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<PostgresContext>(o =>
        {
            o.UseNpgsql(builder.Configuration["db"]);
        });

        builder.Services.AddScoped<IConcertService, ConcertService>();
        builder.Services.AddScoped<ITicketService, TicketService>();

        builder.Services.AddTransient<IEmailService, EmailService>();

        var app = builder.Build();
        var emailPassword = builder.Configuration["emailpassword"];

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapControllers();

        app.Run();
    }
}
