using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[Inheritable]
public record UpgradeComponent : Item
{
    public required IReadOnlyCollection<UpgradeComponentFlag> UpgradeComponentFlags { get; init; }

    public required IReadOnlyCollection<InfusionSlotFlag> InfusionUpgradeFlags { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade Suffix { get; init; }

    public required string SuffixName { get; init; }
}
