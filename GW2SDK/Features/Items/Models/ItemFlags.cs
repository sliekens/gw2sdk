﻿namespace GuildWars2.Items;

/// <summary>Modifiers for items.</summary>
[PublicAPI]
public sealed record ItemFlags
{
    /// <summary>Whether the item becomes account bound when used.</summary>
    public required bool AccountBindOnUse { get; init; }

    /// <summary>Whether the item is always account bound.</summary>
    public required bool AccountBound { get; init; }

    /// <summary>Whether the item is attuned. Attuned equipment gains one extra infusion slot.</summary>
    public required bool Attuned { get; init; }

    public required bool BulkConsume { get; init; }

    /// <summary>Whether attempting to delete the item shows a confirmation dialog.</summary>
    public required bool DeleteWarning { get; init; }

    /// <summary>Whether the item name includes the suffix from any upgrades applied to it.</summary>
    public required bool HideSuffix { get; init; }

    /// <summary>Whether the item is infused. Infused equipment gains one extra infusion slot.</summary>
    public required bool Infused { get; init; }

    public required bool MonsterOnly { get; init; }

    /// <summary>Whether the item can be used in the mystic forge.</summary>
    public required bool NoMysticForge { get; init; }

    /// <summary>Whether the item can be salvaged.</summary>
    public required bool NoSalvage { get; init; }

    /// <summary>Whether the item can be sold to a merchant.</summary>
    public required bool NoSell { get; init; }

    /// <summary>Whether the item can be upgraded with an upgrade component.</summary>
    public required bool NotUpgradeable { get; init; }

    /// <summary>Whether the item can be used while the player is underwater.</summary>
    public required bool NoUnderwater { get; init; }

    /// <summary>Whether the item becomes soulbound when used.</summary>
    public required bool SoulbindOnUse { get; init; }

    /// <summary>Whether the item is always soulbound.</summary>
    public required bool Soulbound { get; init; }

    public required bool Tonic { get; init; }

    /// <summary>Whether the item is unique, preventing the player from equipping more than one.</summary>
    public required bool Unique { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}