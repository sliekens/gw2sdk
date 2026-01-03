namespace GuildWars2.Pve.Raids;

/// <summary>Information about a raid, which is instanced PvE content designed for squads of ten players.</summary>
[DataTransferObject]
public sealed record Raid
{
    /// <summary>The raid ID.</summary>
    public required string Id { get; init; }

    /// <summary>The raid wings. Each raid consists of one or more wings, which can be entered and completed independently. </summary>
    public required IReadOnlyList<RaidWing> Wings { get; init; }
}
