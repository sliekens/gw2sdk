using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWing
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<RaidWingEvent> Events { get; init; }
}
