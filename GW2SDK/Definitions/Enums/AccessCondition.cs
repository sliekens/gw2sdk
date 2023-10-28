using System.ComponentModel;

namespace GuildWars2;

/// <summary>Enumerates the possible access conditions for an expansion.</summary>
[PublicAPI]
[DefaultValue(NoAccess)]
public enum AccessCondition
{
    /// <summary>Indicates no access to an expansion.</summary>
    NoAccess,

    /// <summary>Indicates access to an expansion.</summary>
    HasAccess
}
