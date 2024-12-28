using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a crafting material.</summary>
[PublicAPI]
[JsonConverter(typeof(CraftingMaterialJsonConverter))]
public sealed record CraftingMaterial : Item, IInfusable
{
    /// <inheritdoc />
    public bool Equals(CraftingMaterial? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other) && UpgradesInto.SequenceEqual(other.UpgradesInto));
    }

    /// <summary>If the current crafting material is used in the Mystic Forge to infuse or attune equipment, this collection
    /// contains the IDs of the infused (or attuned) items. Each item in the collection represents a different recipe.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        foreach (var upgrade in UpgradesInto)
        {
            hash.Add(upgrade);
        }

        return hash.ToHashCode();
    }
}
