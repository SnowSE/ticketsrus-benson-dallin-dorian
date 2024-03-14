using Aspose.Html;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using WebApiTRU.Components;
using WebApiTRU.Email;
using WebApiTRU.Services;

public class Program
{
    private static void Main(string[] args)
    {

        IConfiguration cfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.AddHealthChecks();
        builder.Services.AddLogging();

        //they used two, this is one
        /* Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(cfig)
                 .WriteTo.OpenTelemetry(options =>
                 {
                     options.Endpoint = $"{cfig.GetValue<string>("Otlp:Endpoint")}/v1/logs";
                     options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                     options.ResourceAttributes = new Dictionary<string, object>
                     {
                         ["service.name"] = cfig.GetValue<string>("Otlp:ServiceName")
                     };
                 })
                 .CreateLogger();*/
        //This is the other
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = $"{cfig.GetValue<string>("Otlp:Endpoint")}/v1/logs";
                options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
#pragma warning disable CS8601 // Possible null reference assignment.
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = cfig.GetValue<string>("Otlp:ServiceName")
                };
#pragma warning restore CS8601 // Possible null reference assignment.
            }));

#pragma warning disable CS8604 // Possible null reference argument.
        Action<ResourceBuilder> appResourceBuilder =
            resource => resource
                .AddTelemetrySdk()
                .AddService(cfig.GetValue<string>("Otlp:ServiceName"));
#pragma warning restore CS8604 // Possible null reference argument.

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(appResourceBuilder)
            .WithTracing(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("APITracing")
                //.AddConsoleExporter()
                .AddOtlpExporter(options => options.Endpoint = new Uri(cfig.GetValue<string>("Otlp:Endpoint")))
            )
            .WithMetrics(builder => builder
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = new Uri(cfig.GetValue<string>("Otlp:Endpoint"))));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Logger.LogInformation("Benson logged this in program.cs");

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapControllers();
        app.MapHealthChecks("/helathcheck", new HealthCheckOptions
        {
            AllowCachingResponses = false,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            }
        });

        app.Run();
    }
}