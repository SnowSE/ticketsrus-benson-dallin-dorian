// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Internal;
// using Microsoft.Extensions.Logging.Configuration;
// //using Examples.AspNetCore;
// using OpenTelemetry.Exporter;
// using OpenTelemetry.Exporter.Zipkin;
// using OpenTelemetry.Instrumentation.AspNetCore;
// using OpenTelemetry.Logs;
// using OpenTelemetry.Metrics;
// using OpenTelemetry.Resources;
// using OpenTelemetry.Trace;
// using System.Diagnostics;
// //The below using directives all came from: https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/examples/AspNetCore/Program.cs
// using System.Diagnostics.Metrics;
// using System.Drawing.Text;
// using WebMeters;
// //using OpenTelemetry.Exporter.Prometheus.HttpListener;


// var builder = WebApplication.CreateBuilder(args);



// //I already had the code below:

// // Add services to the container.
// builder.Services.AddControllers();
// // Register required services
// /*builder.Services.AddOpenApiDocument(config =>
// config.Title = "AwesomeBlazor API");*/

// builder.Services.AddRazorComponents()
//     .AddInteractiveServerComponents();

// /*builder.Services.AddEndpointsApiExplorer();*/
// builder.Services.AddHttpClient();

// builder.Services.AddEndpointsApiExplorer(); //added for swagger
// builder.Services.AddSwaggerGen();
// builder.Services.AddDbContextFactory<PostgresContext>(c => c.UseNpgsql(builder.Configuration["database"]));
// builder.Services.AddLogging();

// const string serviceName = "webService";

// builder.Logging.AddOpenTelemetry(options =>
// {
//   options.SetResourceBuilder(
//       ResourceBuilder.CreateDefault()
//           .AddService(serviceName))
//       .AddConsoleExporter()
//       .AddOtlpExporter(config =>
//           {
//             config.Endpoint = new Uri("http://rachelotel:4317/");
//           });
// });
// builder.Services.AddOpenTelemetry()
//     .ConfigureResource(resource =>
//           resource.AddService(serviceName))
//     .WithTracing(tracing => tracing
//         .AddAspNetCoreInstrumentation()
//         .AddSource(TraceClass.LoadTicketsPageTraceName)
//         .ConfigureResource(resource =>
//             resource.AddService(serviceName: TraceClass.PurchaseTicketTraceName, serviceVersion: TraceClass.LoadTicketsPageTraceVersion))


//         .AddSource(TraceClass.PurchaseTicketTraceName)
//         .ConfigureResource(resource =>
//             resource.AddService(
//                 serviceName: TraceClass.PurchaseTicketTraceName,
//                 serviceVersion: TraceClass.PurchaseTicketTraceVersion
//             ))
//         .AddOtlpExporter(opt =>
//         {
//           opt.Endpoint = new Uri("http://rachelotel:4317/"); //I changed this port
//         })
//         .AddConsoleExporter())



//     .WithMetrics(metrics => metrics
//         .AddMeter(ActivityMeter.MeterName)
//         .AddAspNetCoreInstrumentation()
//         .AddConsoleExporter()
//         .AddPrometheusExporter()
//         .AddOtlpExporter(opt =>
//         {
//           opt.Endpoint = new Uri("http://rachelotel:4317/");
//         }));


// var app = builder.Build();

// app.UseOpenTelemetryPrometheusScrapingEndpoint();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {

//   app.UseExceptionHandler("/Error", createScopeForErrors: true);
//   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//   app.UseHsts();
// }
// app.UseSwagger();
// app.UseSwaggerUI(); //add swagger
// //app.UseSwaggerUI(c =>
// //{
// //    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
// //});
// app.MapControllers();
// app.UseHttpsRedirection();

// app.MapGet("/health", () =>
// {
//   return "healthy";
// });

// app.UseRouting();
// app.UseStaticFiles();
// app.UseOpenApi();
// app.UseAntiforgery();

// app.MapRazorComponents<App>()
//     .AddInteractiveServerRenderMode();

// app.Run();
// // public partial class Program { }
// // public partial class Startup { }