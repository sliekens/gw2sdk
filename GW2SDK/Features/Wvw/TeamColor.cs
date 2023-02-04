using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2.Wvw;

[PublicAPI]
[DefaultValue(Neutral)]
public enum TeamColor
{
    Neutral,

    Blue,

    Red,

    Green
}
