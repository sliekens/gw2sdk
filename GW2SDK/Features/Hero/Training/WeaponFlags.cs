namespace GuildWars2.Hero.Training;

/// <summary>Modifiers for weapons.</summary>
[PublicAPI]
public sealed record WeaponFlags : Flags
{
    /// <summary>Whether the weapon can be equipped in mainhand.</summary>
    public required bool Mainhand { get; init; }

    /// <summary>Whether the weapon can be equipped in offhand.</summary>
    public required bool Offhand { get; init; }

    /// <summary>Whether the weapon is two-handed.</summary>
    public required bool TwoHand { get; init; }

    /// <summary>Whether the weapon is used underwater.</summary>
    public required bool Aquatic { get; init; }
}
