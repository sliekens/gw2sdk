namespace GuildWars2.Builds.Skills;

/// <summary>A heal skill.</summary>
[PublicAPI]
public sealed record HealSkill : Skill
{
    /// <summary>Used for engineer skills to indicate the ID of the associated toolbelt skill.</summary>
    public required int? ToolbeltSkillId { get; init; }

    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>If present, the current skill will give the player a bundle that replaces the weapon skills with the skill IDs
    /// in this list.</summary>
    public required IReadOnlyList<int>? BundleSkillIds { get; init; }

    /// <summary>If present, the skill is divided into subskills with alternate functions. For example Elementalist glyps have
    /// different effects based on the active attunement.</summary>
    public required IReadOnlyList<Subskill>? SubskillIds { get; init; }
}
