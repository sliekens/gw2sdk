using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlot
{
    public required IReadOnlyCollection<InfusionSlotFlag> Flags { get; init; }

    public required int? ItemId { get; init; }
}
