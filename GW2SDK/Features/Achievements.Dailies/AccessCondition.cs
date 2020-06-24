using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DefaultValue(NoAccess)]
    public enum AccessCondition
    {
        NoAccess,

        HasAccess
    }
}
