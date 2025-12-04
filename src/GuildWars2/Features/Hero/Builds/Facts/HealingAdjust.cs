namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The skill pulses heals when a condition is met.</summary>
public sealed record HealingAdjust : Fact
{
    /// <summary>How many times the skill pulses heals when a condition is met.</summary>
    public required int HitCount { get; init; }
}
