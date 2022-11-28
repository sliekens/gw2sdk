using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record DefaultUpgradeComponent : UpgradeComponent
{
    public required IReadOnlyCollection<string>? Bonuses { get; init; }
}
