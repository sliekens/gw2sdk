namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a map bonus in World vs. World.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Bonus
{
    /// <summary>The bonus kind.</summary>
    public required BonusKind Kind { get; init; }

    /// <summary>The color of the team that owns the bonus.</summary>
    public required TeamColor Owner { get; init; }
}
