using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2;

/// <summary>Describes the desired program behavior when a source JSON document does not exactly match the target CLR type.</summary>
[PublicAPI]
[DefaultValue(Error)]
[JsonConverter(typeof(MissingMemberBehaviorJsonConverter))]
public enum MissingMemberBehavior
{
    /// <summary>Throws an error for unexpected JSON properties and for polymorphic types when the JSON can't be matched to any
    /// derived type.</summary>
    /// <remarks>Use this when correctness of the data is more important than availability. Exceptions may start to occur when
    /// new data is added to the game. GW2SDK must be updated to use the new data.</remarks>
    Error,

    /// <summary>Ignores unexpected JSON properties and uses the closest super type for polymorphic types when the JSON can't
    /// be matched to any derived type.</summary>
    /// <remarks>Use this when availability of the data is more important than correctness. JSON details may be lost when new
    /// data is added to the game. GW2SDK must be updated to get the full representation.</remarks>
    Undefined
}
