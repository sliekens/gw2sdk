using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record Dungeon
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<DungeonPath> Paths { get; init; }
}
