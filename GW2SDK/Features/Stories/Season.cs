using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Season
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<int> StoryIds { get; init; }
}
