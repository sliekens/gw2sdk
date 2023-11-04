namespace GuildWars2.Builds;

/// <summary>The base type for skills.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skill
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the skill.</summary>
    public required string Name { get; init; }

    /// <summary>The list of skill behaviors. For example, if the current skill is a ranged attack, this list will contain a
    /// <see cref="Facts.Range" /> to indicate the maximum range. The list type is abstract, the derived types are documented
    /// in the Facts namespace.</summary>
    public required IReadOnlyCollection<Fact>? Facts { get; init; }

    /// <summary>Some specialization traits can alter skill <see cref="Facts" />, replacing behaviors with new behaviors. This
    /// list contains overrides that apply when a certain trait is equipped.</summary>
    public required IReadOnlyCollection<TraitedFact>? TraitedFacts { get; init; }

    /// <summary>The description as it appears in the tooltip of the skill.</summary>
    public required string Description { get; init; }

    /// <summary>The icon as it appears in the tooltip of the skill.</summary>
    public required string? Icon { get; init; }

    /// <summary>Indicates the type of weapon associated with this skill. Typically this is the weapon you need to equip to
    /// access the skill. It is also used to indicate the weapon type of skills granted by bundles or transformations. If the
    /// skill is not associated with any weapon, like pet skills, this property is <see cref="WeaponType.None" />. It can also
    /// be <c>null</c> for NPC skills or passive trait skills.</summary>
    public required WeaponType? WeaponType { get; init; }

    /// <summary>Indicates profession restrictions for this skill. This list can be empty for stolen skills or skills granted
    /// by bundles or transformations. If the skill has a parent skill then its list of profession restrictions should be used
    /// instead of this one. Otherwise an empty list should be interpreted as no restrictions. It can also be <c>null</c> for
    /// NPC skills or passive trait skills.</summary>
    public required IReadOnlyCollection<ProfessionName>? Professions { get; init; }

    /// <summary>Indicates which slot this skill may occupy. This property is <c>null</c> for NPC skills or passive trait
    /// skills.</summary>
    public required SkillSlot? Slot { get; init; }

    /// <summary>The chat code of the skill. This can be used to link the skill in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }

    // TODO: convert to class
    /// <summary>Contains various modifiers that affect how skills behave.</summary>
    public required IReadOnlyCollection<SkillFlag> SkillFlag { get; init; }

    /// <summary>The skill category as displayed in the tooltip.</summary>
    public required IReadOnlyCollection<SkillCategoryName>? Categories { get; init; }

    /// <summary>Used for skills that are replaced by another skill (flipped) when used, this indicates the ID of the new
    /// skill. For example, the Necromancer's "Reaper's Shroud" skill becomes "Exit Reaper's Shroud".</summary>
    public required int? FlipSkill { get; init; }

    /// <summary>Used for skills that are part of a chain, this indicates the ID of the next skill in the chain.</summary>
    public required int? NextChain { get; init; }

    /// <summary>Used for skills that are part of a chain, this indicates the ID of the previous skill in the chain.</summary>
    public required int? PreviousChain { get; init; }

    /// <summary>The ID of which elite specialization is required to access this skill, or <c>null</c> if no elite
    /// specialization is required.</summary>
    public required int? Specialization { get; init; }
}
