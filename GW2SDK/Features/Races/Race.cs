using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Races;

[PublicAPI]
[DataTransferObject]
public sealed record Race
{
    public required IReadOnlyCollection<int> Skills { get; init; }

    public required RaceName Id { get; init; }

    public required string Name { get; init; }
}
