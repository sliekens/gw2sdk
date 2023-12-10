namespace GuildWars2.Items;

/// <summary>Modifiers for upgrade components.</summary>
[PublicAPI]
public sealed record UpgradeComponentFlags
{
    public required bool HeavyArmor { get; init; }

    public required bool MediumArmor { get; init; }

    public required bool LightArmor { get; init; }

    public required bool Axe { get; init; }

    public required bool Dagger { get; init; }

    public required bool Focus { get; init; }

    public required bool Greatsword { get; init; }

    public required bool Hammer { get; init; }

    public required bool HarpoonGun { get; init; }

    public required bool LongBow { get; init; }

    public required bool Mace { get; init; }

    public required bool Pistol { get; init; }

    public required bool Rifle { get; init; }

    public required bool Scepter { get; init; }

    public required bool Shield { get; init; }

    public required bool ShortBow { get; init; }

    public required bool Spear { get; init; }

    public required bool Staff { get; init; }

    public required bool Sword { get; init; }

    public required bool Torch { get; init; }

    public required bool Trident { get; init; }

    public required bool Trinket { get; init; }

    public required bool Warhorn { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
