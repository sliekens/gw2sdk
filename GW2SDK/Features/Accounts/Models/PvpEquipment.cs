using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Models;

[PublicAPI]
[DataTransferObject]
public sealed record PvpEquipment
{
    /// <summary>The ID of the selected amulet.</summary>
    public int? AmuletId { get; init; }

    /// <summary>The ID of the selected rune.</summary>
    public int? RuneId { get; init; }

    /// <summary>The IDs of all equipped sigils.</summary>
    public IReadOnlyCollection<int?> SigilIds { get; init; } = Array.Empty<int?>();
}
