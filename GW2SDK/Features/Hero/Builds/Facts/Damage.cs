namespace GuildWars2.Hero.Builds.Facts;

/// <summary>An amount of damage applied by the skill/trait.</summary>
[PublicAPI]
public sealed record Damage : Fact
{
    /// <summary>How many times the damage is applied.</summary>
    public required int HitCount { get; init; }

    /// <summary>Some multiplier for the damage.</summary>
    public required double DamageMultiplier { get; init; }
}
