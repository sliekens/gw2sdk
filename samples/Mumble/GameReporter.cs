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

        // BackgroundService starts synchronously, i.e. app startup is delayed until the first await
        await Task.Yield();

        // Initialize the shared memory link
        using var gameLink = GameLink.Open();

        // The game link emits the current player's map ID and specialization ID
        // Additional information about those can be retrieved from the API
        var maps = await gw2.Maps.GetMaps(cancellationToken: stoppingToken);
        var mapsDictionary = maps.Value.ToDictionary(map => map.Id);

        var specializations =
            await gw2.Specializations.GetSpecializations(cancellationToken: stoppingToken);
        var specializationsDictionary =
            specializations.Value.ToDictionary(specialization => specialization.Id);

        gameLink.Subscribe(
            snapshot =>
            {
                var pos = snapshot.AvatarPosition;

                if (!snapshot.TryGetIdentity(out var identity, MissingMemberBehavior.Error))
                {
                    return;
                }

                if (!snapshot.TryGetContext(out var context))
                {
                    return;
                }

                var specialization = "no specialization";
                if (specializationsDictionary.TryGetValue(identity.SpecializationId, out var found))
                {
                    specialization = found.Name;
                }

                var map = mapsDictionary[identity.MapId];
                var activity = "traveling";
                if (!context.UiState.HasFlag(UiState.GameHasFocus))
                {
                    activity = "afk-ing";
                }
                else if (context.UiState.HasFlag(UiState.TextboxHasFocus))
                {
                    activity = "typing";
                }
                else if (context.UiState.HasFlag(UiState.IsMapOpen))
                {
                    activity = "looking at the map";
                }
                else if (context.UiState.HasFlag(UiState.IsInCombat))
                {
                    activity = "in combat";
                }

                logger.LogInformation(
                    "[{UiTick}] {Name}, the {Race} {Profession} ({Specialization}) is {Activity} on {Transport} in {Map}, Position: {{ Right = {Pos0}, Up = {Pos1}, Front = {Pos2} }}",
                    snapshot.UiTick,
                    identity.Name,
                    identity.Race,
                    identity.Profession,
                    specialization,
                    activity,
                    context.IsMounted ? context.GetMount() : "foot",
                    map.Name,
                    pos[0],
                    pos[1],
                    pos[2]
                );
            },
            stoppingToken
        );

        // Wait indefinitely until the application is stopped
        await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
    }
}
