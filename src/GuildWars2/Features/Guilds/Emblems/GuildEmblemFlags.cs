namespace GuildWars2.Guilds.Emblems;

/// <summary>Modifiers for guild emblems.</summary>
public sealed record GuildEmblemFlags : Flags
{
    /// <summary>The background image is flipped horizontally.</summary>
    public required bool FlipBackgroundHorizontal { get; init; }

    /// <summary>The background image is flipped vertically.</summary>
    public required bool FlipBackgroundVertical { get; init; }
}
