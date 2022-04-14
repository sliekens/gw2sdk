using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Banking.Models;

[PublicAPI]
[DataTransferObject]
public sealed record BankSlot
{
    public int Id { get; init; }

    public int Count { get; init; }

    public int? Charges { get; init; }

    public int? Skin { get; init; }

    public IReadOnlyCollection<int>? Upgrades { get; init; }

    /// <summary>Indicates which upgrade slots are in use. (0-based)</summary>
    public IReadOnlyCollection<int>? UpgradeSlotIndices { get; init; }

    public IReadOnlyCollection<int>? Infusions { get; init; }

    // Always length 4
    public IReadOnlyCollection<int>? Dyes { get; init; }

    public ItemBinding Binding { get; init; }

    /// <summary>The name of the character when the item is Soulbound.</summary>
    public string BoundTo { get; init; } = "";

    public SelectedStat? Stats { get; init; }
}