using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record DefaultGizmo : Gizmo
{
    public required IReadOnlyCollection<int>? VendorIds { get; init; }

    public required int? GuildUpgradeId { get; init; }
}
