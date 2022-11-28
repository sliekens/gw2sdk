using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Worlds;

[PublicAPI]
[DataTransferObject]
public sealed record World
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required WorldPopulation Population { get; init; }
}
