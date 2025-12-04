namespace GuildWars2.Items;

/// <summary>Modifiers for upgrade components.</summary>
public sealed record UpgradeComponentFlags : Flags
{
    /// <summary>Indicates if the upgrade component can be applied to Heavy Armor.</summary>
    public required bool HeavyArmor { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to Medium Armor.</summary>
    public required bool MediumArmor { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to Light Armor.</summary>
    public required bool LightArmor { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to an Axe.</summary>
    public required bool Axe { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Dagger.</summary>
    public required bool Dagger { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Focus.</summary>
    public required bool Focus { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Greatsword.</summary>
    public required bool Greatsword { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Hammer.</summary>
    public required bool Hammer { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Harpoon Gun.</summary>
    public required bool HarpoonGun { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Long Bow.</summary>
    public required bool LongBow { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Mace.</summary>
    public required bool Mace { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Pistol.</summary>
    public required bool Pistol { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Rifle.</summary>
    public required bool Rifle { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Scepter.</summary>
    public required bool Scepter { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Shield.</summary>
    public required bool Shield { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Short Bow.</summary>
    public required bool ShortBow { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Spear.</summary>
    public required bool Spear { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Staff.</summary>
    public required bool Staff { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Sword.</summary>
    public required bool Sword { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Torch.</summary>
    public required bool Torch { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Trident.</summary>
    public required bool Trident { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Trinket.</summary>
    public required bool Trinket { get; init; }

    /// <summary>Indicates if the upgrade component can be applied to a Warhorn.</summary>
    public required bool Warhorn { get; init; }
}
