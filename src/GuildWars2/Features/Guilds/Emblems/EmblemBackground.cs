namespace GuildWars2.Guilds.Emblems;

/// <summary>Information about a guild emblem background.</summary>
[DataTransferObject]
public sealed record EmblemBackground
{
    /// <summary>The background ID.</summary>
    public required int Id { get; init; }

    /// <summary>The image URLs of the background layers.</summary>
    public required IReadOnlyList<Uri> LayerUrls { get; init; }
}
