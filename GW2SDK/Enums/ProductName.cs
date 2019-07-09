using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum ProductName
    {
        // Should never happen according to wiki (https://wiki.guildwars2.com/index.php?title=API:2/account&oldid=1865452)
        None = 0,

        PlayForFree,

        GuildWars2,

        HeartOfThorns,

        PathOfFire
    }
}