using System.Text.Json.Serialization;

using GuildWars2.Chat;

namespace GuildWars2.Items;

/// <summary>Information about a transmutation consumable, which unlocks an equipment skin upon receipt. The item can be
/// consumed to change the appearance of one item without costing a transmutation charge.</summary>
[JsonConverter(typeof(TransmutationJsonConverter))]
public sealed record Transmutation : Consumable
{
    /// <summary>The IDs of the equipment skins that are unlocked when this item is received.</summary>
    /// <remarks>Armor skins may unlock an identical skin for every weight class, each having its own ID.</remarks>
    public required IImmutableValueList<int> SkinIds { get; init; }

    /// <summary>Gets chat link objects for the skins.</summary>
    /// <returns>The chat links as objects.</returns>
    public IEnumerable<SkinLink> GetSkinChatLinks()
    {
        return SkinIds.Select(id => new SkinLink { SkinId = id });
    }
}
