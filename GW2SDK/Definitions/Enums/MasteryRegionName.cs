using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(Unknown)]
public enum MasteryRegionName
{
    Unknown,

    Tyria,

    Maguuma,

    Desert,

    Tundra
}
