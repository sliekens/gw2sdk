namespace GuildWars2.Hero.Emotes;

/// <summary>Information about an emote.</summary>
[DataTransferObject]
public sealed record Emote
{
    /// <summary>The emote ID.</summary>
    public required string Id { get; init; }

    /// <summary>The chat commands that can be used to trigger the emote. This includes translated commands for French, German
    /// and Spanish.</summary>
    public required IReadOnlyCollection<string> Commands { get; init; }

    /// <summary>The IDs of items that unlock the emote when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
