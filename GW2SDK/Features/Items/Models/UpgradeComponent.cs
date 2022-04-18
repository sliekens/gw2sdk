using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
[Inheritable]
public record UpgradeComponent : Item
{
    public IReadOnlyCollection<UpgradeComponentFlag> UpgradeComponentFlags { get; init; } =
        Array.Empty<UpgradeComponentFlag>();

    public IReadOnlyCollection<InfusionSlotFlag> InfusionUpgradeFlags { get; init; } =
        Array.Empty<InfusionSlotFlag>();

    public double AttributeAdjustment { get; init; }

    public InfixUpgrade Suffix { get; init; } = new();

    public string SuffixName { get; init; } = "";
}
