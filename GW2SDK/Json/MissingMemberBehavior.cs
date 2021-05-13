using System.ComponentModel;
using GW2SDK.Annotations;

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
