using GuildWars2;
using GuildWars2.Exploration.Maps;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Worlds;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mumble;

internal sealed class DataService(
    ILogger<DataService> logger,
    Gw2Client gw2Client,
    ReferenceData referenceData
) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (MapSummary map in await gw2Client.Exploration
            .GetMapSummaries(cancellationToken: cancellationToken)
            .ValueOnly())
        {
            if (!referenceData.TryAddMap(map))
            {
                logger.LogWarning("Map {Name} ({Id}) could not be added.", map.Name, map.Id);
            }
        }

        foreach (Specialization specialization in await gw2Client.Hero.Builds
            .GetSpecializations(cancellationToken: cancellationToken)
            .ValueOnly())
        {
            if (!referenceData.TryAddSpecialization(specialization))
            {
                logger.LogWarning(
                    "Specialization {Name} ({Id}) could not be added.",
                    specialization.Name,
                    specialization.Id
                );
            }
        }

        foreach (DyeColor color in await gw2Client.Hero.Equipment.Dyes
            .GetColors(cancellationToken: cancellationToken)
            .ValueOnly())
        {
            if (!referenceData.TryAddColor(color))
            {
                logger.LogWarning("Color {Name} ({Id}) could not be added.", color.Name, color.Id);
            }
        }

        foreach (World world in await gw2Client.Worlds.GetWorlds(cancellationToken: cancellationToken)
            .ValueOnly())
        {
            if (!referenceData.TryAddWorld(world))
            {
                logger.LogWarning("World {Name} ({Id}) could not be added.", world.Name, world.Id);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
