using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DailyAchievement
    {
        public int Id { get; init; }

        public LevelRequirement Level { get; init; } = new();

        public ProductRequirement? RequiredAccess { get; init; }
    }
}
