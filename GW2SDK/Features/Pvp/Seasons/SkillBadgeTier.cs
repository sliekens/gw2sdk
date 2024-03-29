namespace GuildWars2.Pvp.Seasons;

/// <summary>Information about a tier within a skill badge rank.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillBadgeTier
{
    /// <summary>The minimum skill rating required for this tier.</summary>
    public required int Rating { get; init; }
}
