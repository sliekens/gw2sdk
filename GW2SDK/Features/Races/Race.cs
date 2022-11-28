using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Races;

[PublicAPI]
[DataTransferObject]
public sealed record Race
{
    public required IReadOnlyCollection<int> Skills { get; init; }

    public required RaceName Id { get; init; }

    public required string Name { get; init; }
}
