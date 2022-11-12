using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Legends;

[PublicAPI]
[DataTransferObject]
public sealed record Legend
{
    public required string Id { get; init; }

    public required int Code { get; init; }

    public required int Swap { get; init; }

    public required int Heal { get; init; }

    public required int Elite { get; init; }

    public required IReadOnlyCollection<int> Utilities { get; init; }
}
