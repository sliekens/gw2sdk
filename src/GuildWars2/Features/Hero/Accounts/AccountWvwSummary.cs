namespace GuildWars2.Hero.Accounts;

/// <summary>Information about a player account's World vs. World team and rank.</summary>
[DataTransferObject]
public sealed record AccountWvwSummary
{
    /// <summary>The player's World vs. World team ID.</summary>
    public required int? TeamId { get; init; }

    /// <summary>The account's personal World vs. World rank. Requires the 'progression' scope.</summary>
    public required int? Rank { get; init; }
}
