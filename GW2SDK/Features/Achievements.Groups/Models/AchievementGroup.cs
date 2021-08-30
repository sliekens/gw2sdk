using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record AchievementGroup
    {
        public string Id { get; init; } = "";

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";

        public int Order { get; init; }

        public int[] Categories { get; init; } = Array.Empty<int>();
    }
}
