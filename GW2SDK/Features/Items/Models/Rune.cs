using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record Rune : UpgradeComponent
{
    public IReadOnlyCollection<string>? Bonuses { get; init; }
}
