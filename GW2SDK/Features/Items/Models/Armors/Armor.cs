using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[Inheritable]
public record Armor : Item
{
    public required int DefaultSkin { get; init; }

    public required WeightClass WeightClass { get; init; }

    public required int Defense { get; init; }

    public required IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade? Prefix { get; init; }

    public required int? SuffixItemId { get; init; }

    public required IReadOnlyCollection<int>? StatChoices { get; init; }
}
