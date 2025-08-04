namespace GuildWars2.Hero.Masteries;

/// <summary>Information about mastery points earned and spent in a region.</summary>
public sealed record MasteryPointsTotal
{
    /// <summary>The region corresponding to the mastery points.</summary>
    public required Extensible<MasteryRegionName> Region { get; init; }

    /// <summary>The number of mastery points spent in the region.</summary>
    public required int Spent { get; init; }

    /// <summary>The number of mastery points earned in the region.</summary>
    public required int Earned { get; init; }

    /// <summary>The number of unspent mastery points in the region.</summary>
    public int Available => Earned - Spent;
}
