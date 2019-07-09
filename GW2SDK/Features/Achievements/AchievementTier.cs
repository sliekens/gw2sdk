using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class AchievementTier
    {
        public int Count { get; set; }

        public int Points { get; set; }
    }
}