namespace GuildWars2.Guilds.Emblems;

/// <summary>Modifiers for guild emblems.</summary>
[PublicAPI]
public sealed record GuildEmblemFlags
{
    /// <summary>The background image is flipped horizontally.</summary>
    public required bool FlipBackgroundHorizontal { get; init; }

    /// <summary>The background image is flipped vertically.</summary>
    public required bool FlipBackgroundVertical { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
