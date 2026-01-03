namespace GuildWars2.Wvw.Upgrades;

/// <summary>Information about an upgrade effect provided by an upgrade tier.</summary>
[DataTransferObject]
public sealed record Upgrade
{
    /// <summary>The upgrade name.</summary>
    public required string Name { get; init; }

    /// <summary>The upgrade description.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the upgrade icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the upgrade icon.</summary>
    public required Uri IconUrl { get; init; }
}
