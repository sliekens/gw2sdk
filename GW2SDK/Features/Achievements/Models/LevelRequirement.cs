using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record LevelRequirement
    {
        public int Min { get; init; } = 1;

        public int Max { get; init; } = 80;
    }
}
