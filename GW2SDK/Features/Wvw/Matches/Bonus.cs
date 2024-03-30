namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a map bonus in World vs. World.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Bonus
{
    /// <summary>The bonus kind.</summary>
    public required Extensible<BonusKind> Kind { get; init; }

    /// <summary>The color of the team that owns the bonus.</summary>
    public required Extensible<TeamColor> Owner { get; init; }
}
