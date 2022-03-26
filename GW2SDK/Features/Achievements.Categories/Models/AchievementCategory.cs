using System.Collections.Generic;
using System.Linq;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record AchievementCategory
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";

        public int Order { get; init; }

        public string Icon { get; init; } = "";

        public IEnumerable<AchievementRef> Achievements { get; init; } = Enumerable.Empty<AchievementRef>();

        public IEnumerable<AchievementRef> Tomorrow { get; init; } = Enumerable.Empty<AchievementRef>();
    }

    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record AchievementRef
    {
        public int Id { get; init; }
    }
}
