using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [DefaultValue(Small)]
    public enum UiSize
    {
        Small,

        Normal,

        Large,

        Larger
    }
}
