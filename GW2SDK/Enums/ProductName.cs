using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum ProductName
    {
        // Should never happen according to wiki (https://wiki.guildwars2.com/index.php?title=API:2/account&oldid=1865452)
        None,

        PlayForFree,

        GuildWars2,

        HeartOfThorns,

        PathOfFire
    }
}
