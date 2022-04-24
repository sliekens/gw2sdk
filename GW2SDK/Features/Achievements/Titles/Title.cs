using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Titles;

[PublicAPI]
[DataTransferObject]
public sealed record Title
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public IReadOnlyCollection<int>? Achievements { get; init; }

    public int? AchievementPointsRequired { get; init; }
}
