using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Mumble;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mumble;

public class GameReporter : BackgroundService
{
    private readonly Gw2Client gw2;

    private readonly ILogger<GameReporter> logger;

    public GameReporter(ILogger<GameReporter> logger, Gw2Client gw2)
    {
        this.logger = logger;
        this.gw2 = gw2;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!GameLink.IsSupported())
        {
            throw new NotSupportedException();
        }

        // Workaround for synchronous starts (BackgroundService delays app startup until the first await)
        await Task.Yield();

        // Prepare a dictionary of maps
        var maps = await gw2.Maps.GetMaps(cancellationToken: stoppingToken);
        var mapsDictionary = maps.Value.ToDictionary(map => map.Id);

        // Prepare a dictionary of elite specializations
        var specializations =
            await gw2.Specializations.GetSpecializations(cancellationToken: stoppingToken);
        var specializationsDictionary =
            specializations.Value.ToDictionary(specialization => specialization.Id);

        // Initialize the shared memory link
        using var gameLink = GameLink.Open();

        // Subscribe to the GameLink observable
        // I recommend using Rx (System.Reactive) instead of implementing IObserver<T> yourself
        gameLink.Subscribe(
            tick =>
            {
                // This callback is executed whenever the game client updates the shared memory
                var identity = tick.GetIdentity();
                if (identity is null)
                {
                    // Identity might be unavailable when the game client is not running
                    // Extremely rarely it might be unavailable due to concurrent reads and writes
                    return;
                }

                var specialization = "no specialization";
                if (specializationsDictionary.TryGetValue(identity.SpecializationId, out var found))
                {
                    specialization = found.Name;
                }

                var map = mapsDictionary[identity.MapId];

                var title = $"the {identity.Race} {identity.Profession}";
                if (!tick.Context.UiState.HasFlag(UiState.GameHasFocus))
                {
                    logger.LogInformation(
                        "[{UiTick}] {Name}, {Title} ({Specialization}) is afk",
                        tick.UiTick,
                        identity.Name,
                        title,
                        specialization
                    );
                }
                else if (tick.Context.UiState.HasFlag(UiState.TextboxHasFocus))
                {
                    logger.LogInformation(
                        "[{UiTick}] {Name}, {Title} ({Specialization}) is typing",
                        tick.UiTick,
                        identity.Name,
                        title,
                        specialization
                    );
                }
                else if (tick.Context.UiState.HasFlag(UiState.IsMapOpen))
                {
                    logger.LogInformation(
                        "[{UiTick}] {Name}, {Title} ({Specialization}) is looking at the map",
                        tick.UiTick,
                        identity.Name,
                        title,
                        specialization
                    );
                }
                else if (tick.Context.UiState.HasFlag(UiState.IsInCombat))
                {
                    logger.LogInformation(
                        "[{UiTick}] {Name}, {Title} ({Specialization}) is in combat",
                        tick.UiTick,
                        identity.Name,
                        title,
                        specialization
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
                        specialization,
                        transport,
                        map.Name,
                        tick.Context.MapType.ToString(),
                        tick.AvatarPosition.X,
                        tick.AvatarPosition.Z,
                        tick.AvatarPosition.Y
                    );
                }
            },
            err =>
            {
                logger.LogError(err, "Something went wrong.");
            },
            stoppingToken
        );

        // Wait indefinitely until the application is stopped
        await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
    }
}
