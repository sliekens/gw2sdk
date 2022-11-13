using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementGroup
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<int> Categories { get; init; }
}
