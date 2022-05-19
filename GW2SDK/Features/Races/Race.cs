using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Races;

[PublicAPI]
[DataTransferObject]
public sealed record Race
{
    public IReadOnlyCollection<int> Skills = Array.Empty<int>();

    public RaceName Id { get; init; }

    public string Name { get; init; } = "";
}
