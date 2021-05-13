using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Json
{
    [PublicAPI]
    [DefaultValue(Undefined)]
    public enum MissingMemberBehavior
    {
        Undefined,

        Error
    }
}
