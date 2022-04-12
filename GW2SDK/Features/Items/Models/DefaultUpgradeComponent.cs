using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record DefaultUpgradeComponent : UpgradeComponent
{
    public IReadOnlyCollection<string>? Bonuses { get; init; }
}