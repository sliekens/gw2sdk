using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a ring.</summary>
[PublicAPI]
[JsonConverter(typeof(RingJsonConverter))]
public sealed record Ring : Trinket
{
    /// <summary>If the current ring can be infused or attuned, this collection contains the IDs of the infused/attuned
    /// variations of the ring. Each item in the collection represents a possible upgrade path.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <summary>If the current ring is infused or attuned, this collection contains the IDs of possible source items.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradeSource> UpgradesFrom { get; init; }

    /// <inheritdoc />
    public bool Equals(Ring? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other)
                && UpgradesInto.SequenceEqual(other.UpgradesInto)
                && UpgradesFrom.SequenceEqual(other.UpgradesFrom));
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

        foreach (var source in UpgradesFrom)
        {
            hash.Add(source);
        }

        return hash.ToHashCode();
    }
}
