using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record Specialization
{
    public required int? Id { get; init; }

    // Always length 3
    public required IReadOnlyCollection<int?> Traits { get; init; }
}
