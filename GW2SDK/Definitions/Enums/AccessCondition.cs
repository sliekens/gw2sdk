using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(NoAccess)]
public enum AccessCondition
{
    NoAccess,

    HasAccess
}
