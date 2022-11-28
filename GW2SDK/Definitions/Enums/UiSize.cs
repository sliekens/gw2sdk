using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(Small)]
public enum UiSize
{
    Small,

    Normal,

    Large,

    Larger
}
