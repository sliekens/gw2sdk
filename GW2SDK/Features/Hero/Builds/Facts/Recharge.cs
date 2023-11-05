namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The recharge time of the skill/trait.</summary>
[PublicAPI]
public sealed record Recharge : Fact
{
    /// <summary>The recharge duration of the skill/trait.</summary>
    public required TimeSpan Duration { get; init; }
}
