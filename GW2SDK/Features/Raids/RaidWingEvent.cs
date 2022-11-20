using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWingEvent
{
    public required string Id { get; init; }

    public required RaidWingEventKind Kind { get; init; }
}
