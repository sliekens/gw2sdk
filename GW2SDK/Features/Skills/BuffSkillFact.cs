namespace GuildWars2.Skills;

[PublicAPI]
[Inheritable]
public record BuffSkillFact : SkillFact
{
    /// <summary>The duration of the effect applied by the skill, or null when the effect is removed by the skill.</summary>
    public required TimeSpan? Duration { get; init; }

    public required string Status { get; init; }

    public required string Description { get; init; }

    /// <summary>The number of stacks applied by the skill, or null when the effect is removed by the skill.</summary>
    public required int? ApplyCount { get; init; }
}
