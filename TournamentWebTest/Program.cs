using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TournamentWebTest;
using TournamentWebTest.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<TableTestService>();
builder.Services.AddScoped<ArenaCup2026TestService>();
builder.Services.AddScoped<HartmutLayer2026TestService>();
builder.Services.AddScoped<IBackupFileService, BackupFileService>();
builder.Services.AddScoped<SimulationService>();

await builder.Build().RunAsync();
