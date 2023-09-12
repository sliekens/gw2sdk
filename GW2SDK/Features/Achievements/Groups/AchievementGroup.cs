using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Groups;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementGroup
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<int> Categories { get; init; }
}
