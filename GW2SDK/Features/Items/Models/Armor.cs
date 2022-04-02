using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public record Armor : Item
    {
        public int DefaultSkin { get; init; }

        public WeightClass WeightClass { get; init; }

        public int Defense { get; init; }

        public IReadOnlyCollection<InfusionSlot> InfusionSlots { get; init; } = Array.Empty<InfusionSlot>();

        public double AttributeAdjustment { get; init; }

        public InfixUpgrade? Prefix { get; init; }

        public int? SuffixItemId { get; init; }

        public IReadOnlyCollection<int>? StatChoices { get; init; }
    }
}
