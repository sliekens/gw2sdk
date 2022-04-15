using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record Transmutation : Consumable
{
    public IReadOnlyCollection<int> Skins { get; init; } = Array.Empty<int>();
}
