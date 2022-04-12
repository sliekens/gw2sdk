using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlot
{
    public IReadOnlyCollection<InfusionSlotFlag> Flags { get; init; } = Array.Empty<InfusionSlotFlag>();

    public int? ItemId { get; init; }
}