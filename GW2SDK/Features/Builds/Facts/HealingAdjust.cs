namespace GuildWars2.Builds.Facts;

/// <summary>The skill pulses heals when a condition is met.</summary>
[PublicAPI]
public sealed record HealingAdjust : Fact
{
    /// <summary>How many times the skill pulses heals when a condition is met.</summary>
    public required int HitCount { get; init; }
}
