namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Modifiers for skins.</summary>
[PublicAPI]
public sealed record SkinFlags
{
    /// <summary>Whether the skin is hidden until it is unlocked.</summary>
    public required bool HideIfLocked { get; init; }

    /// <summary>Whether the skin is free to apply, for example Hellfire armor skins from achievements.</summary>
    public required bool NoCost { get; init; }

    /// <summary>Whether the skin overrides item rarity when applied.</summary>
    public required bool OverrideRarity { get; init; }

    /// <summary>Whether the skin can be found in the wardrobe panel.</summary>
    public required bool ShowInWardrobe { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
