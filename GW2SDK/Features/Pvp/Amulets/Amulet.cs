using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Amulets;

[PublicAPI]
[DataTransferObject]
public sealed record Amulet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required IDictionary<AttributeAdjustTarget, int> Attributes { get; init; }
}
