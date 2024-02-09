using Microsoft.EntityFrameworkCore;
using WebApiTRU.Components;
using LibraryTRU.Data;
using WebApiTRU.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSingleton<IConcertService, ConcertService>();
builder.Services.AddSingleton<ITicketService, TicketService>();

builder.Services.AddDbContext<PostgresContext>(o => 
{
    o.UseNpgsql(builder.Configuration["db"]);
    
});

var app = builder.Build();

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
