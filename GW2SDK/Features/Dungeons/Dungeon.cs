using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record Dungeon
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<DungeonPath> Paths { get; init; }
}
