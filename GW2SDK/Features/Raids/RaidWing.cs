using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWing
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<RaidWingEvent> Events { get; init; }
}
