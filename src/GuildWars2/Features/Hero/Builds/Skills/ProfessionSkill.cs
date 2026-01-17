namespace GuildWars2.Hero.Builds.Skills;

/// <summary>Information about a skill belonging to a profession mechanic.</summary>
public sealed record ProfessionSkill : ActionSkill
{
    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Extensible<Attunement>? Attunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>If present, the current skill will transform the player and replace the weapon skills with the skill IDs in
    /// this list.</summary>
    public required IImmutableValueList<int>? TransformSkills { get; init; }
}
