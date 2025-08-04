namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A boon, condition or effect applied (or removed) by the skill/trait.</summary>
[Inheritable]
public record Buff : Fact
{
    /// <summary>The duration of the effect applied by the skill/trait, or null when the effect is removed by the skill/trait.</summary>
    public required TimeSpan? Duration { get; init; }

    /// <summary>The name of the boon, condition or effect applied or removed by the skill/trait. For example: "Might".</summary>
    public required string Status { get; init; }

    /// <summary>The description as it appears in the skill/trait tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>The number of stacks applied by the skill/trait, or null when the effect is removed by the skill/trait.</summary>
    public required int? ApplyCount { get; init; }
}
