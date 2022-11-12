using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Titles;

[PublicAPI]
[DataTransferObject]
public sealed record Title
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<int>? Achievements { get; init; }

    public required int? AchievementPointsRequired { get; init; }
}
