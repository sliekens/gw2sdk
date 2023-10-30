namespace GuildWars2.Inventories;

/// <summary>Information about bags equipped by a character on the account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    /// <summary>The bags equipped by the character. Empty slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<Bag?> Bags { get; init; }
}
