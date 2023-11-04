namespace GuildWars2.Builds.Skills;

/// <summary>An elite skill.</summary>
[PublicAPI]
public sealed record EliteSkill : Skill
{
    /// <summary>Used for engineer utility skills to indicate the associated toolbelt skill.</summary>
    public required int? ToolbeltSkillId { get; init; }

    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>If present, the current skill will give the player a bundle that replaces the weapon skills with the skill IDs
    /// in this list.</summary>
    public required IReadOnlyCollection<int>? BundleSkillIds { get; init; }

    /// <summary>If present, the current skill will transform the player and replace the weapon skills with the skill IDs in
    /// this list.</summary>
    public required IReadOnlyCollection<int>? TransformSkillIds { get; init; }

    /// <summary>If present, the skill is divided into subskills. For example some Elementalist skills behave differently based
    /// on the current attunement and some Druid skills are different when in Celestial Avatar form.</summary>
    public required IReadOnlyCollection<SkillReference>? SubskillIds { get; init; }
}
