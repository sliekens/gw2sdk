﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    public int ColorId { get; init; }

    public Material Material { get; init; }
}