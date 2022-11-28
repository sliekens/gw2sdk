using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record CraftingMaterial : Item
{
    public required IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }
}
