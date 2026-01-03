namespace GuildWars2.Quaggans;

/// <summary>Information about a quaggan.</summary>
[DataTransferObject]
public sealed record Quaggan
{
    /// <summary>The quaggan ID.</summary>
    public required string Id { get; init; }

    /// <summary>The quaggan's picture URL.</summary>
    public required Uri ImageUrl { get; init; }
}
