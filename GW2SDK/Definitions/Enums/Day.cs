using System.ComponentModel;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(Today)]
public enum Day
{
    Today,

    Tomorrow
}
