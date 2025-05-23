using System.Text.Json.Serialization;
using GuildWars2.Chat;

namespace GuildWars2.Hero.Equipment.Outfits;

/// <summary>Information about an outfit.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(OutfitJsonConverter))]
public sealed record Outfit
{
    /// <summary>The outfit ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the outfit.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the outfit icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the outfit icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The IDs of the items that unlock the outfit when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>Gets a chat link object for this outfit.</summary>
    /// <returns>The chat link as an object.</returns>
    public OutfitLink GetChatLink()
    {
        return new OutfitLink { OutfitId = Id };
    }
}
