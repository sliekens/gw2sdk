namespace GuildWars2.Hero.Builds;

/// <summary>Information about a Revenant legend.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Legend
{
    /// <summary>The legend ID.</summary>
    public required string Id { get; init; }

    /// <summary>The legend's code which is used in build template chat links.</summary>
    public required int Code { get; init; }

    /// <summary>The skill ID of the legend's swap skill.</summary>
    public required int Swap { get; init; }

    /// <summary>The skill ID of the legend's healing skill.</summary>
    public required int Heal { get; init; }

    /// <summary>The skill ID of the legend's elite skill.</summary>
    public required int Elite { get; init; }

    /// <summary>The skill IDs of the legend's utility skills.</summary>
    public required IReadOnlyCollection<int> Utilities { get; init; }
}
