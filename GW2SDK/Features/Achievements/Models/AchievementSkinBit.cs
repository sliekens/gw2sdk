using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record AchievementSkinBit : AchievementBit
    {
        public int Id { get; init; }
    }
}
