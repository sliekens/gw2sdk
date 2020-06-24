using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum AchievementFlag
    {
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
