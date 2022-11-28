using System.Collections.Generic;
using GuildWars2.Annotations;
using GuildWars2.ItemStats;
using JetBrains.Annotations;

namespace GuildWars2.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record ItemSlot
{
    public required int Id { get; init; }

    public required int Count { get; init; }

    public required int? Charges { get; init; }

    public required int? Skin { get; init; }

    public required IReadOnlyCollection<int>? Upgrades { get; init; }

    /// <summary>Indicates which upgrade slots are in use. (0-based)</summary>
    public required IReadOnlyCollection<int>? UpgradeSlotIndices { get; init; }

    public required IReadOnlyCollection<int>? Infusions { get; init; }

    // Always length 4
    public required IReadOnlyCollection<int?>? Dyes { get; init; }

    public required ItemBinding Binding { get; init; }

    /// <summary>The name of the character when the item is Soulbound.</summary>
    public required string BoundTo { get; init; }

    public required SelectedStat? Stats { get; init; }
}
