using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record DefaultGizmo : Gizmo
{
    public required IReadOnlyCollection<int>? VendorIds { get; init; }

    public required int? GuildUpgradeId { get; init; }
}
