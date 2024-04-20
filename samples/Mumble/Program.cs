using System;
using System.Linq;
using System.Threading;
using GuildWars2;
using GuildWars2.Mumble;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mumble;

// Using Microsoft.Extensions.Hosting is optional,
// but it makes it easier to configure logging, dependency injection and cancellation
var app = CreateApplication();
var stoppingToken = app.GetStoppingToken();

var logger = app.GetService<ILogger<GameLink>>();

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException();
}

// Prepare a dictionary of MapSummary and Specialization for later use
var gw2 = app.GetService<Gw2Client>();

var maps = await gw2.Exploration.GetMapSummaries(cancellationToken: stoppingToken)
    .AsDictionary(static map => map.Id)
    .ValueOnly();

var specializations = await gw2.Hero.Builds.GetSpecializations()
    .AsDictionary(static specialization => specialization.Id)
    .ValueOnly();

// Initialize the shared memory link
var refreshInterval = TimeSpan.FromSeconds(1);
var gameLink = GameLink.Open(refreshInterval);

// Subscribe to the GameLink observable
// I recommend using Rx (System.Reactive) instead of implementing IObserver<T> yourself
gameLink.Subscribe(

    // OnNext(GameTick) is executed whenever the game client updates the shared memory
    tick =>
    {
        var identity = tick.GetIdentity();
        if (identity is null)
        {
            // Identity might be null due to invalid JSON
            // (happens very rarely due to concurrent reads/writes)
            return;
        }

        var currentSpecialization = "no specialization";
        if (specializations.TryGetValue(identity.SpecializationId, out var specialization))
        {
            currentSpecialization = specialization.Name;
        }

        var currentMap = "an unknown map";
        if (maps.TryGetValue(identity.MapId, out var map))
        {
            currentMap = map.Name;
        }

        var title = $"the {identity.Race} {identity.Profession}";
        if (!tick.Context.UiState.HasFlag(UiState.GameHasFocus))
        {
            logger.LogInformation(
                "[{UiTick}] {Name}, {Title} ({Specialization}) is afk",
                tick.UiTick,
                identity.Name,
                title,
                currentSpecialization
            );
        }
        else if (tick.Context.UiState.HasFlag(UiState.TextboxHasFocus))
        {
            logger.LogInformation(
                "[{UiTick}] {Name}, {Title} ({Specialization}) is typing",
                tick.UiTick,
                identity.Name,
                title,
                currentSpecialization
            );
        }
        else if (tick.Context.UiState.HasFlag(UiState.IsMapOpen))
        {
            logger.LogInformation(
                "[{UiTick}] {Name}, {Title} ({Specialization}) is looking at the map",
                tick.UiTick,
                identity.Name,
                title,
                currentSpecialization
            );
        }
        else if (tick.Context.UiState.HasFlag(UiState.IsInCombat))
        {
            logger.LogInformation(
                "[{UiTick}] {Name}, {Title} ({Specialization}) is in combat",
                tick.UiTick,
                identity.Name,
                title,
                currentSpecialization
            );
        }
        else
        {
            var transport = tick.Context.IsMounted ? tick.Context.Mount.ToString() : "foot";
            logger.LogInformation(
                "[{UiTick}] {Name}, {Title} ({Specialization}) is on {Transport} in {Map} ({Type}), Position: {{ Latitude = {X}, Longitude = {Z}, Elevation = {Y} }}",
                tick.UiTick,
                identity.Name,
                title,
                currentSpecialization,
                transport,
                currentMap,
                tick.Context.MapType.ToString(),
                tick.AvatarPosition.X,
                tick.AvatarPosition.Z,
                tick.AvatarPosition.Y
            );
        }
    },

    // OnError(Exception) is executed when there is an internal error while reading from the shared memory
    // or when your OnNext callback throws an exception, it will also end up here
    err =>
    {
        logger.LogError(err, "Something went wrong.");
        app.StopAsync();
    },

    // OnComplete callback runs when you unsubscribe or when the GameLink is being disposed
    () =>
    {
        logger.LogInformation("Stopping on tick {UiTick}", gameLink.GetSnapshot().UiTick);
    },
    stoppingToken
);

try
{
    await app.RunAsync(stoppingToken);
}
catch (OperationCanceledException)
{
}

IHost CreateApplication()
{
    var host = Host.CreateApplicationBuilder(args);

    // Configure services
    host.Services.AddHttpClient<Gw2Client>();

    // Configure logging
    host.Logging.AddSimpleConsole(
        options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss.fff ";
            options.UseUtcTimestamp = true;
        }
    );

    return host.Build();
}

namespace Mumble
{
    internal static class HostExtensions
    {
        public static T GetService<T>(this IHost app) where T : notnull =>
            app.Services.GetRequiredService<T>();

        public static CancellationToken GetStoppingToken(this IHost app) =>
            app.GetService<IHostApplicationLifetime>().ApplicationStopping;
    }
}
