using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record Raid
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<RaidWing> Wings { get; init; }
}
