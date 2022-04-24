using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record Dungeon
{
    public string Id { get; init; } = "";

    public IReadOnlyCollection<DungeonPath> Paths { get; init; } = Array.Empty<DungeonPath>();
}
