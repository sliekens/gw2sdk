using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Transmutation : Consumable
{
    public required IReadOnlyCollection<int> Skins { get; init; }
}
