using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record CraftingMaterial : Item
{
    public IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }
}
