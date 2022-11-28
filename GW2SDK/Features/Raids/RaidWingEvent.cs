using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWingEvent
{
    public required string Id { get; init; }

    public required RaidWingEventKind Kind { get; init; }
}
