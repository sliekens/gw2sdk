﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    public required int ColorId { get; init; }

    public required Material Material { get; init; }
}
