using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.SkillChallenge;

[PublicAPI]
[DataTransferObject]
public sealed record SkillChallenge
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
