using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record AchievementItemBit : AchievementBit
    {
        public int Id { get; init; }
    }
}
