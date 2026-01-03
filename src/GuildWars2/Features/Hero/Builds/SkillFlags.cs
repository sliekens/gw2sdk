namespace GuildWars2.Hero.Builds;

/// <summary>Modifiers for skills.</summary>
public sealed record SkillFlags : Flags
{
    /// <summary>The skill is a ground targeting skill with a range indicator.</summary>
    public required bool GroundTargeted { get; init; }

    /// <summary>The skill can't be used while swimming.</summary>
    public required bool NoUnderwater { get; init; }
}
