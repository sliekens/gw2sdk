using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a crafting material.</summary>
[JsonConverter(typeof(CraftingMaterialJsonConverter))]
public sealed record CraftingMaterial : Item, IInfusable
{
    /// <summary>If the current crafting material is used in the Mystic Forge to infuse or attune equipment, this collection
    /// contains the IDs of the infused (or attuned) items. Each item in the collection represents a different recipe.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }
}
