﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record DungeonPath
{
    public string Id { get; init; } = "";

    public DungeonKind Kind { get; init; }
}