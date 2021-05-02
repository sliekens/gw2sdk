using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DailyAchievementProductRequirement
    {
        public ProductName Product { get; init; }

        public AccessCondition Condition { get; init; }
    }
}
