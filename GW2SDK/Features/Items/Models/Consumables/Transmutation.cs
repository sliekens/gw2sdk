using System.Text.Json.Serialization;
using GuildWars2.Chat;

namespace GuildWars2.Items;

/// <summary>Information about a transmutation consumable, which unlocks an equipment skin upon receipt. The item can be
/// consumed to change the appearance of one item without costing a transmutation charge.</summary>
[PublicAPI]
[JsonConverter(typeof(TransmutationJsonConverter))]
public sealed record Transmutation : Consumable
{
    /// <summary>The IDs of the equipment skins that are unlocked when this item is received.</summary>
    /// <remarks>Armor skins may unlock an identical skin for every weight class, each having its own ID.</remarks>
    public required IReadOnlyCollection<int> SkinIds { get; init; }

    /// <inheritdoc />
    public bool Equals(Transmutation? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other) && SkinIds.SequenceEqual(other.SkinIds));
    }

    /// <summary>Gets chat link objects for the skins.</summary>
    /// <returns>The chat links as objects.</returns>
    public IEnumerable<SkinLink> GetSkinChatLinks()
    {
        return SkinIds.Select(id => new SkinLink { SkinId = id });
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        foreach (var skinId in SkinIds)
        {
            hash.Add(skinId);
        }

        return hash.ToHashCode();
    }
}
