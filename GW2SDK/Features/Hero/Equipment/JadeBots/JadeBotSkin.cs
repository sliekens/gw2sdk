using System.Text.Json.Serialization;
using GuildWars2.Chat;

namespace GuildWars2.Hero.Equipment.JadeBots;

/// <summary>Information about a jade bot skin.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(JadeBotSkinJsonConverter))]
public sealed record JadeBotSkin
{
    /// <summary>The jade bot skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The jade bot skin name.</summary>
    public required string Name { get; init; }

    /// <summary>The jade bot skin description.</summary>
    public required string Description { get; init; }

    /// <summary>The ID of the item which grants this jade bot skin when consumed.</summary>
    public required int UnlockItemId { get; init; }

    /// <summary>Gets a chat link object for this jade bot skin (unlock item).</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink()
    {
        return new ItemLink { ItemId = UnlockItemId };
    }
}
