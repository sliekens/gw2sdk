namespace GuildWars2.Skills.Facts;

/// <summary>An amount of damage applied by the skill.</summary>
[PublicAPI]
public sealed record Damage : SkillFact
{
    /// <summary>How many times the damage is applied.</summary>
    public required int HitCount { get; init; }

    /// <summary>Some multiplier for the damage.</summary>
    public required double DamageMultiplier { get; init; }
}
