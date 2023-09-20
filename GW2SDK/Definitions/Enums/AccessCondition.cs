using System.ComponentModel;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(NoAccess)]
public enum AccessCondition
{
    NoAccess,

    HasAccess
}
