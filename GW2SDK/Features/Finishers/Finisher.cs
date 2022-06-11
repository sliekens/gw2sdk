using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Finishers;

[PublicAPI]
[DataTransferObject]
public sealed record Finisher
{
    public int Id { get; init; }

    /// <remarks>Can be empty.</remarks>
    public string UnlockDetails { get; init; } = "";

    /// <remarks>Can be empty.</remarks>
    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();

    public int Order { get; init; }

    public string Icon { get; init; } = "";

    public string Name { get; init; } = "";
}
