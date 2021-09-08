using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Http
{
    [PublicAPI]
    [DefaultValue(Miss)]
    public enum ResponseCacheDecision
    {
        Miss,

        Hit,

        Validate
    }
}
