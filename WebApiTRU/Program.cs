using Microsoft.EntityFrameworkCore;
using WebApiTRU.Components;
using WebApiTRU.Email;
using WebApiTRU.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry;
using System.Diagnostics.Metrics;
using OpenTelemetry.Exporter.Zipkin;


public class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddHealthChecks();
        builder.Services.AddLogging();

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(Constants.serviceName))
                    .AddConsoleExporter()
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri("http://otel-collector:4317/");
                    });
        });

        var greeterMeter = new Meter(Constants.serviceName, Constants.serviceVersion);

        builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                    resource.AddService(
                        serviceName: Constants.serviceName,
                        serviceVersion: Constants.serviceVersion))
            .WithTracing(b => b
                .AddAspNetCoreInstrumentation()
                .AddSource(Constants.sourceName)
                .ConfigureResource(r => r
                    .AddService(serviceName: Constants.serviceName, serviceVersion: Constants.serviceVersion))
                .AddSource(Constants.sourceName)
                .ConfigureResource(resource =>
                resource.AddService(serviceName: Constants.serviceName, serviceVersion: Constants.serviceVersion
                )).AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://otel-collector:4317/");
                })
                .AddConsoleExporter())


            .WithMetrics(metrics => metrics
                .AddMeter(greeterMeter.Name) //custom meter name
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .AddPrometheusExporter()
                .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri("http://otel-collector:4317/");
                    }));





        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllers();
        builder.Services.AddDbContextFactory<PostgresContext>(o =>
        {
            o.UseNpgsql(builder.Configuration["db"]);
        }, ServiceLifetime.Scoped);

        builder.Services.AddScoped<IConcertService, ConcertService>();
        builder.Services.AddScoped<ITicketService, TicketService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

        var app = builder.Build();

        app.UseOpenTelemetryPrometheusScrapingEndpoint();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // app.MapHealthChecks("/health", new HealthCheckOptions
        // {
        //     AllowCachingResponses = false,
        //     ResultStatusCodes =
        //         {
        //             [HealthStatus.Healthy] = StatusCodes.Status200OK,
        //             [HealthStatus.Degraded] = StatusCodes.Status200OK,
        //             [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        //         }
        // });

        app.MapGet("/health", () =>
        {
            return "healthy";
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseAuthorization();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapControllers();

        app.Run();
    }
}
