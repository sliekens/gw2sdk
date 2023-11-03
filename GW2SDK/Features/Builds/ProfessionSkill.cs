using GuildWars2.Builds.Skills;

namespace GuildWars2.Builds;

/// <summary>Information about a skill belonging to a profession mechanic.</summary>
[PublicAPI]
public sealed record ProfessionSkill : Skill
{
    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>If present, the current skill will transform the player and replace the weapon skills with the skill
    /// IDs in this list.</summary>
    public required IReadOnlyList<int>? TransformSkills { get; init; }
}
