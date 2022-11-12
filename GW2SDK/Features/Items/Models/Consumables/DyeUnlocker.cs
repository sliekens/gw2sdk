﻿using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record DyeUnlocker : Unlocker
{
    public required int ColorId { get; init; }
}
