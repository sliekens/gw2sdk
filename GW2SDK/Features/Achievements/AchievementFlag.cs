using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public enum AchievementFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(AchievementFlag)
        CategoryDisplay = 1,

        Daily,

        Hidden,

        IgnoreNearlyComplete,

        MoveToTop,

        Pvp,

        RepairOnLogin,

        Repeatable,

        RequiresUnlock,

        Permanent
    }
}