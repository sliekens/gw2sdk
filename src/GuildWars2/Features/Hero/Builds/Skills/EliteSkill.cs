namespace GuildWars2.Hero.Builds.Skills;

/// <summary>An elite skill.</summary>
public sealed record EliteSkill : SlotSkill
{
    /// <summary>If present, the current skill will transform the player and replace the weapon skills with the skill IDs in
    /// this list.</summary>
    public required IReadOnlyList<int>? TransformSkillIds { get; init; }
}
