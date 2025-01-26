using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a ring.</summary>
[PublicAPI]
[JsonConverter(typeof(RingJsonConverter))]
public sealed record Ring : Trinket, IInfused, IInfusable
{
    /// <summary>If the current ring can be infused or attuned in the Mystic Forge, this collection contains the IDs of the
    /// infused (or attuned) variations of the ring. Each item in the collection represents a different recipe.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <summary>If the current ring is infused or attuned, this collection contains the IDs of possible source items.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradeSource> UpgradesFrom { get; init; }
}
