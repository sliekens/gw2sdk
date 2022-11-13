using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
[DataTransferObject]
public sealed record PvpEquipment
{
    /// <summary>The ID of the selected amulet.</summary>
    public required int? AmuletId { get; init; }

    /// <summary>The ID of the selected rune.</summary>
    public required int? RuneId { get; init; }

    /// <summary>The IDs of all equipped sigils.</summary>
    public required IReadOnlyCollection<int?> SigilIds { get; init; }
}
