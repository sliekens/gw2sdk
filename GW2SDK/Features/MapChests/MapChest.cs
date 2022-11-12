﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.MapChests;

[PublicAPI]
[DataTransferObject]
public sealed record MapChest
{
    public required string Id { get; init; }
}
