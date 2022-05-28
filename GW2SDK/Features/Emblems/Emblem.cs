using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Emblems;

[PublicAPI]
[DataTransferObject]
public sealed record Emblem
{
    public int Id { get; init; }

    public IReadOnlyCollection<string> Layers { get; init; } = Array.Empty<string>();
}
