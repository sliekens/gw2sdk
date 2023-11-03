namespace GuildWars2.Skills.Facts;

/// <summary>A boon, condition or effect applied (or removed) by the skill.</summary>
[PublicAPI]
[Inheritable]
public record Buff : SkillFact
{
    /// <summary>The duration of the effect applied by the skill, or null when the effect is removed by the skill.</summary>
    public required TimeSpan? Duration { get; init; }

    /// <summary>The name of the boon, condition or effect applied or removed by the skill.</summary>
    public required string Status { get; init; }

    /// <summary>The description as it appears in the skill tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>The number of stacks applied by the skill, or null when the effect is removed by the skill.</summary>
    public required int? ApplyCount { get; init; }
}
