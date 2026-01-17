namespace GuildWars2.Hero.Builds;

/// <summary>The base type for skills which are activated by the player to perform an action. Cast objects of this type to
/// a more specific type to access more properties.</summary>
[Inheritable]
[DataTransferObject]
public record ActionSkill : Skill
{
    /// <summary>Indicates the type of weapon associated with this skill. This is typically the weapon you need to equip to
    /// access the skill. It is also used to indicate the weapon type of skills granted by bundles or transformations. If the
    /// skill is not associated with any weapon, like pet skills, this property is <see cref="WeaponType.None" />.</summary>
    public required Extensible<WeaponType> WeaponType { get; init; }

    /// <summary>Indicates profession restrictions for this skill. This list can be empty for stolen skills or skills granted
    /// by bundles or transformations. If the skill has a parent skill then its list of profession restrictions should be used
    /// instead of this one. Otherwise, an empty list should be interpreted as no restrictions.</summary>
    public required IImmutableValueList<Extensible<ProfessionName>> Professions { get; init; }

    /// <summary>Indicates which slot this skill may occupy.</summary>
    public required Extensible<SkillSlot> Slot { get; init; }

    /// <summary>Used for skills that are replaced by another skill (flipped) when used, this indicates the ID of the new
    /// skill. For example, the Necromancer's "Reaper's Shroud" skill becomes "Exit Reaper's Shroud".</summary>
    public required int? FlipSkillId { get; init; }

    /// <summary>Used for skills that are part of a chain, this indicates the ID of the next skill in the chain.</summary>
    public required int? NextSkillId { get; init; }

    /// <summary>Used for skills that are part of a chain, this indicates the ID of the previous skill in the chain.</summary>
    public required int? PreviousSkillId { get; init; }

    /// <summary>The ID of which elite specialization is required to access this skill, or <c>null</c> if no elite
    /// specialization is required.</summary>
    public required int? SpecializationId { get; init; }
}
