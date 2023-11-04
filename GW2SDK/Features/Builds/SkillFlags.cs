namespace GuildWars2.Builds;

/// <summary>Modifiers for skills.</summary>
[PublicAPI]
public sealed record SkillFlags
{
    /// <summary>The skill is a ground targeting skill with a range indicator.</summary>
    public required bool GroundTargeted { get; init; }

    /// <summary>The skill can't be used while swimming.</summary>
    public required bool NoUnderwater { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
