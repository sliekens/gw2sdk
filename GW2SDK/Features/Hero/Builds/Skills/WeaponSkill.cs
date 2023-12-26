namespace GuildWars2.Hero.Builds.Skills;

/// <summary>A weapon skill.</summary>
[PublicAPI]
public sealed record WeaponSkill : Skill
{
    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Weaver skills to indicate which secondary attunement this skill is associated with.</summary>
    public required Attunement? DualAttunement { get; init; }

    /// <summary>Used for Revenant, Warrior and Druid skills to indicate their cost.</summary>
    public required int? Cost { get; init; }

    /// <summary>Used for Dual Wield skills to indicate which off-hand weapon must be equipped.</summary>
    public required Offhand? Offhand { get; init; }

    /// <summary>Used for Thief skills to indicates the Initiative cost.</summary>
    public required int? Initiative { get; init; }
}
