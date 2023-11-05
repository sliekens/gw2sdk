namespace GuildWars2.Hero.Achievements;

/// <summary>Describes minimum/maximum levels for achievements.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record LevelRequirement
{
    /// <summary>The minimum level required to access this achievement.</summary>
    public required int Min { get; init; }

    /// <summary>The maximum level required to access this achievement.</summary>
    public required int Max { get; init; }
}
