using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Accounts
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