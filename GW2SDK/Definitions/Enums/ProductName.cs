using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
[DefaultValue(None)]
public enum ProductName
{
    // Should never happen according to wiki (https://wiki.guildwars2.com/index.php?title=API:2/account&oldid=1865452)
    None,

    PlayForFree,

    GuildWars2,

    HeartOfThorns,

    PathOfFire,

    EndOfDragons
}
