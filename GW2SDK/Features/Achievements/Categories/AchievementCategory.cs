using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementCategory
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public int Order { get; init; }

    public string Icon { get; init; } = "";

    public IReadOnlyCollection<AchievementRef> Achievements { get; init; } =
        Array.Empty<AchievementRef>();

    public IReadOnlyCollection<AchievementRef>? Tomorrow { get; init; }
}

[PublicAPI]
[DataTransferObject]
public sealed record AchievementRef
{
    public int Id { get; init; }

    public ProductRequirement? RequiredAccess { get; init; }

    public IReadOnlyCollection<AchievementFlag>? Flags { get; init; }

    public LevelRequirement? Level { get; init; }
}
