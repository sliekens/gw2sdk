using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
[DataTransferObject]
public sealed record EquipmentTab
{
    /// <summary>The number of the current tab.</summary>
    public required int Tab { get; init; }

    /// <summary>The player-chosen name of this equipment tab.</summary>
    public required string Name { get; init; }

    /// <summary>The selected equipment on this tab.</summary>
    public required IEnumerable<EquipmentItem> Equipment { get; init; }

    /// <summary>The selected PvP equipment on this tab.</summary>
    public required PvpEquipment PvpEquipment { get; init; }
}
