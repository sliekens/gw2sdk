namespace GuildWars2.Builds.Skills;

/// <summary>Used when the skill is altered by equipping a specialization trait.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record TraitedSkillFact
{
    /// <summary>The ID of the trait that activates this skill fact.</summary>
    public required int RequiresTrait { get; init; }

    /// <summary>The index of the fact that is replaced by this <see cref="Fact" />, or <c>null</c> if it is to be appended to
    /// the existing facts.</summary>
    public required int? Overrides { get; init; }

    /// <summary>The skill fact that is added or which replaces another skill fact (if <see cref="Overrides" /> is present).</summary>
    public required SkillFact Fact { get; init; }
}
