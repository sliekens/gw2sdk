using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK;

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

    EdgeOfTheMists,
}
