using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>Describes minimum/maximum levels for achievements.</summary>
[DataTransferObject]
[JsonConverter(typeof(LevelRequirementJsonConverter))]
public sealed record LevelRequirement
{
    /// <summary>The minimum level required to access this achievement.</summary>
    public required int Min { get; init; }

    /// <summary>The maximum level required to access this achievement.</summary>
    public required int Max { get; init; }
}
