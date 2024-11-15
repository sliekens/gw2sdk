namespace GuildWars2.Items;

/// <summary>Information about a crafting material.</summary>
[PublicAPI]
public sealed record CraftingMaterial : Item
{
    /// <summary>If the current material is used in a Mystic Forge recipe to infuse equipment, this collection contains the IDs
    /// of the infused items. Each item in the collection represents a possible upgrade path.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <inheritdoc />
    public bool Equals(CraftingMaterial? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other) && UpgradesInto.SequenceEqual(other.UpgradesInto));
    }

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
