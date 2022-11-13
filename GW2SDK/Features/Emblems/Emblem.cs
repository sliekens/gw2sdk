using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Emblems;

[PublicAPI]
[DataTransferObject]
public sealed record Emblem
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<string> Layers { get; init; }
}
