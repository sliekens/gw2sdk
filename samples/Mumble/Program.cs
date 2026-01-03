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

// Choose an interval to indicate how often you want to
//   receive fresh data from the game.
// For example, at most once every second.
// Default: no limit, every change in the game state
//   will be available immediately.
TimeSpan refreshInterval = GameLink.MinimumRefreshInterval;

// Open the game link with the chosen refresh interval.
// GameLink implements IDiposable and IAsyncDisposable,
//  make sure it is disposed one way or another,
//  e.g. by 'using' or 'await using'.
GameLink gameLink = GameLink.Open(refreshInterval);

await using (gameLink.ConfigureAwait(false))
{
    // Setup dependency injection and logging
    HostApplicationBuilder host = Host.CreateApplicationBuilder(args);
    host.Services.AddSingleton(gameLink);
    host.Services.AddHttpClient<Gw2Client>();
    host.Services.AddHostedService<DataService>();
    host.Services.AddHostedService<GameListener>();
    host.Services.AddSingleton<ReferenceData>();
    host.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss.fff ";
            options.UseUtcTimestamp = true;
        }
    );

    IHost app = host.Build();

    // Start the services and wait until Ctrl+C is pressed
    await app.RunAsync().ConfigureAwait(false);
}
