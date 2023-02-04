using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(Unknown)]
public enum MapKind
{
    Unknown,

    Public,

    Instance,

    JumpPuzzle,

    Tutorial,

    Pvp,

    Center,

    RedHome,

    BlueHome,

    GreenHome,

    EdgeOfTheMists
}
