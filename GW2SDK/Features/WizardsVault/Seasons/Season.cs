namespace GuildWars2.WizardsVault.Seasons;

/// <summary>Information about a Wizard's Vault season.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Season
{
    /// <summary>The title of the objective.</summary>
    public required string Title { get; init; }

    /// <summary>The start of the season (UTC/server time).</summary>
    public required DateTimeOffset Start { get; init; }

    /// <summary>The end of the season (UTC/server time).</summary>
    public required DateTimeOffset End { get; init; }

    /// <summary>The IDs of all Astral Rewards.</summary>
    public required HashSet<int> AstralRewardIds { get; init; }

    /// <summary>The IDs of all objectives.</summary>
    public required HashSet<int> ObjectiveIds { get; init; }
}
