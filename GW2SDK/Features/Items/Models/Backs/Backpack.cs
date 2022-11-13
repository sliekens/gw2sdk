using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Backpack : Item
{
    public required int DefaultSkin { get; init; }

    public required IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade? Prefix { get; init; }

    public required int? SuffixItemId { get; init; }

    public required IReadOnlyCollection<int>? StatChoices { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesFrom { get; init; }
}
