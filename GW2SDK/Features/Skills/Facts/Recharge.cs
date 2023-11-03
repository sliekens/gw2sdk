namespace GuildWars2.Skills.Facts;

/// <summary>The recharge time of the skill.</summary>
[PublicAPI]
public sealed record Recharge : SkillFact
{
    /// <summary>The recharge duration of the skill.</summary>
    public required TimeSpan Duration { get; init; }
}
