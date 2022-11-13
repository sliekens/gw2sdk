using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
[DataTransferObject]
public sealed record Novelty
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public required NoveltyKind Slot { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }
}
