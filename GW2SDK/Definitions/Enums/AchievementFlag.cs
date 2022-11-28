using JetBrains.Annotations;

namespace GuildWars2;

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

    Permanent,

    Weekly,

    SpecialEvent,

    PvE,

    WvW
}
