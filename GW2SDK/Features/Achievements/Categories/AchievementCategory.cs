using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementCategory
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required int Order { get; init; }

    public required string Icon { get; init; }

    public required IReadOnlyCollection<AchievementRef> Achievements { get; init; }

    public required IReadOnlyCollection<AchievementRef>? Tomorrow { get; init; }
}
