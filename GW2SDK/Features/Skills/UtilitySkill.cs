namespace GuildWars2.Skills;

/// <summary>An utility skill.</summary>
[PublicAPI]
public sealed record UtilitySkill : Skill
{
    /// <summary>Used for engineer utility skills to indicate the associated toolbelt skill.</summary>
    public required int? ToolbeltSkill { get; init; }

    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>If present, the current skill will give the player a bundle that replaces the weapon skills with the skill IDs
    /// in this list.</summary>
    public required IReadOnlyCollection<int>? BundleSkills { get; init; }

    public required IReadOnlyCollection<SkillReference>? Subskills { get; init; }
}
