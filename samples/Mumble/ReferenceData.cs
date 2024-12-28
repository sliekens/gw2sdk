using System.Collections.Concurrent;
using GuildWars2.Exploration.Maps;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Worlds;

namespace Mumble;

public sealed class ReferenceData
{
    private readonly ConcurrentDictionary<int, MapSummary> maps = new();

    private readonly ConcurrentDictionary<int, Specialization> specializations = new();

    private readonly ConcurrentDictionary<int, DyeColor> colors = new();

    private readonly ConcurrentDictionary<int, World> worlds= new();

    public IReadOnlyDictionary<int, MapSummary> Maps => maps.AsReadOnly();

    public IReadOnlyDictionary<int, Specialization> Specializations => specializations.AsReadOnly();

    public IReadOnlyDictionary<int, DyeColor> Colors => colors.AsReadOnly();

    public IReadOnlyDictionary<int, World> Worlds=> worlds.AsReadOnly();

    public bool TryAddMap(MapSummary map)
    {
        return maps.TryAdd(map.Id, map);
    }

    public bool TryAddSpecialization(Specialization specialization)
    {
        return specializations.TryAdd(specialization.Id, specialization);
    }

    public bool TryAddColor(DyeColor color)
    {
        return colors.TryAdd(color.Id, color);
    }

    public bool TryAddWorld(World world)
    {
        return worlds.TryAdd(world.Id, world);
    }
}
