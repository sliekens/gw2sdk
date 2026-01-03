namespace GuildWars2.Hero.Builds.Skills;

/// <summary>A weapon skill.</summary>
public sealed record WeaponSkill : ActionSkill
{
    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Extensible<Attunement>? Attunement { get; init; }

    /// <summary>Used for Weaver skills to indicate which secondary attunement this skill is associated with.</summary>
    public required Extensible<Attunement>? DualAttunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>Used for Dual Wield skills to indicate which off-hand weapon must be equipped.</summary>
    public required Extensible<Offhand>? Offhand { get; init; }

    /// <summary>Used for Thief skills to indicates the Initiative cost.</summary>
    public required int? Initiative { get; init; }
}
