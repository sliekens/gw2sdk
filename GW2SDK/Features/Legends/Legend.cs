using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Legends;

[PublicAPI]
[DataTransferObject]
public sealed record Legend
{
    public string Id { get; init; } = "";

    public int Code { get; init; }

    public int Swap { get; init; }

    public int Heal { get; init; }

    public int Elite { get; init; }

    public IReadOnlyCollection<int> Utilities { get; init; } = Array.Empty<int>();
}
