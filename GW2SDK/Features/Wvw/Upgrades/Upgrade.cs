namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
[DataTransferObject]
public sealed record Upgrade
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    /// <summary>The URL of the WvW upgrade icon.</summary>
    public required string IconHref { get; init; }
}
