using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.Adventures;

[PublicAPI]
[DataTransferObject]
public sealed record Adventure
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
