using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlot
{
    public required IReadOnlyCollection<InfusionSlotFlag> Flags { get; init; }

    public required int? ItemId { get; init; }
}
