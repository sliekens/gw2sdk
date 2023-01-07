using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.HeroChallenges;

[PublicAPI]
[DataTransferObject]
public sealed record HeroChallenge
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
