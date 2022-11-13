using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.SkillChallenge;

[PublicAPI]
[DataTransferObject]
public sealed record SkillChallenge
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<double> Coordinates { get; init; }
}
