using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(Today)]
public enum Day
{
    Today,

    Tomorrow
}
