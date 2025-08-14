using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Modifiers for skins.</summary>
[JsonConverter(typeof(SkinFlagsJsonConverter))]
public sealed record SkinFlags : Flags
{
    /// <summary>Whether the skin is hidden until it is unlocked.</summary>
    public required bool HideIfLocked { get; init; }

    /// <summary>Whether the skin is free to apply, for example Hellfire armor skins from achievements.</summary>
    public required bool NoCost { get; init; }

    /// <summary>Whether the skin overrides item rarity when applied.</summary>
    public required bool OverrideRarity { get; init; }

    /// <summary>Whether the skin can be found in the wardrobe panel.</summary>
    public required bool ShowInWardrobe { get; init; }
}
