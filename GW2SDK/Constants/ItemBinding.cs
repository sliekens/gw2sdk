using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum ItemBinding
    {
        None,

        Account,

        Character
    }
}
