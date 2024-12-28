using GuildWars2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mumble;

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException(
        "MumbleLink only works on Windows due to the use of named memory-mapped files."
    );
}

// Initialize the shared memory link with a refresh interval of your choice
// GameLink implements IAsyncDisposable, so you can use it in an async using statement
var refreshInterval = GameLink.MinimumRefreshInterval;
await using var gameLink = GameLink.Open(refreshInterval);

// Setup dependency injection and logging
var host = Host.CreateApplicationBuilder(args);
host.Services.AddSingleton(gameLink);
host.Services.AddHttpClient<Gw2Client>();
host.Services.AddHostedService<DataService>();
host.Services.AddHostedService<GameListener>();
host.Services.AddSingleton<ReferenceData>();
host.Logging.AddSimpleConsole(
    options =>
    {
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss.fff ";
        options.UseUtcTimestamp = true;
    }
);

var app = host.Build();
await app.RunAsync();
