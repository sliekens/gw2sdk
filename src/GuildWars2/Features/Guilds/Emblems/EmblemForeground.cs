namespace GuildWars2.Guilds.Emblems;

/// <summary>Information about a guild emblem foreground.</summary>
[DataTransferObject]
public sealed record EmblemForeground
{
    /// <summary>The foreground ID.</summary>
    public required int Id { get; init; }

    /// <summary>The image URLs of the foreground layers.</summary>
    [Obsolete("Use LayerUrls instead.")]
    public required IReadOnlyList<string> Layers { get; init; }

    /// <summary>The image URLs of the foreground layers.</summary>
    public required IReadOnlyList<Uri> LayerUrls { get; init; }
}
