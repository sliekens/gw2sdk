using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Gliders;

[PublicAPI]
[DataTransferObject]
public sealed record Glider
{
    public int Id { get; init; }

    /// <remarks>Can be empty.</remarks>
    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();

    public int Order { get; init; }

    public string Icon { get; init; } = "";

    public string Name { get; init; } = "";

    /// <remarks>Can be empty.</remarks>
    public string Description { get; init; } = "";

    /// <remarks>Can be empty.</remarks>
    public IReadOnlyCollection<int> DefaultDyes { get; init; } = Array.Empty<int>();
}
