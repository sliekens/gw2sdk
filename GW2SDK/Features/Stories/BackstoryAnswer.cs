using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record BackstoryAnswer
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Journal { get; init; }

    public required int Question { get; init; }

    public required IReadOnlyCollection<RaceName>? Races { get; init; }

    public required IReadOnlyCollection<ProfessionName>? Professions { get; init; }
}
