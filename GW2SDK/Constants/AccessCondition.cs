using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
[DefaultValue(NoAccess)]
public enum AccessCondition
{
    NoAccess,

    HasAccess
}
