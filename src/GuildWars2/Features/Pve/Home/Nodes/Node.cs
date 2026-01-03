namespace GuildWars2.Pve.Home.Nodes;

/// <summary>Information about a gathering node that can be placed in a home instance.</summary>
[DataTransferObject]
public sealed record Node
{
    /// <summary>The node ID.</summary>
    public required string Id { get; init; }
}
