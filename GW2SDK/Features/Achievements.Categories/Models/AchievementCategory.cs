using System;
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

        public int[] Achievements { get; init; } = Array.Empty<int>();
    }
}
