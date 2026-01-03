namespace GuildWars2.Pve.Home.Cats;

/// <summary>Information about a cat that can be added to the home instance.</summary>
[DataTransferObject]
public sealed record Cat
{
    /// <summary>The cat ID.</summary>
    public required int Id { get; init; }

    /// <summary>The hint for finding the cat.</summary>
    public required string Hint { get; init; }
}
