using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementCategory
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    public required int Order { get; init; }

    public required string Icon { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<AchievementRef> Achievements { get; init; }

    public required IReadOnlyCollection<AchievementRef>? Tomorrow { get; init; }
}
