using GuildWars2.Hero.Builds;

namespace GuildWars2.Hero.Training;

/// <summary>Information about a weapon skill.</summary>
[DataTransferObject]
public sealed record WeaponSkill
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>The slot in which the skill is equipped.</summary>
    public required Extensible<SkillSlot> Slot { get; init; }

    /// <summary>Used for Dual Wield skills to indicate which off-hand weapon must be equipped.</summary>
    public required Extensible<Offhand>? Offhand { get; init; }

    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Extensible<Attunement>? Attunement { get; init; }
}
