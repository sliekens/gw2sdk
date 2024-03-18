﻿namespace GuildWars2.Items;

/// <summary>Modifiers for infusion slots.</summary>
[PublicAPI]
public sealed record InfusionSlotFlags : Flags
{
    /// <summary>Whether enchrichment can be used in the slot.</summary>
    public required bool Enrichment { get; init; }

    /// <summary>Whether stat infusions can be used in the slot.</summary>
    public required bool Infusion { get; init; }
}
