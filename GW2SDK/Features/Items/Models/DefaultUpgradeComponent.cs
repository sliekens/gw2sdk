using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record DefaultUpgradeComponent : UpgradeComponent
{
    public IReadOnlyCollection<string>? Bonuses { get; init; }
}