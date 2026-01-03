namespace GuildWars2.Logos;

/// <summary>Information about a logo.</summary>
[DataTransferObject]
public sealed record Logo
{
    /// <summary>The logo ID.</summary>
    public required string Id { get; init; }

    /// <summary>The logo's URL.</summary>
    public required Uri Url { get; init; }
}
