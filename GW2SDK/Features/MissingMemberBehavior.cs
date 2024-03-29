﻿using System.ComponentModel;

namespace GuildWars2;

/// <summary>Describes the desired program behavior when a JSON document does not match the CLR type.</summary>
[PublicAPI]
[DefaultValue(Error)]
public enum MissingMemberBehavior
{
    /// <summary>Throws an error for unexpected JSON properties. Throws an error when string constants can't be converted to an
    /// enum. Throws an error for unexpected or missing type names when the type is polymorphic.</summary>
    /// <remarks>Use this when correctness of the data is more important than availability. Errors may be thrown when new data
    /// is added to the game. GW2SDK must be updated to use the new data.</remarks>
    Error,

    /// <summary>Ignores unexpected JSON properties. Converts unexpected string constants to an enum value with no name.
    /// Replaces unexpected or missing type names with the base class when the type is polymorphic.</summary>
    /// <remarks>Use this when availability of the data is more important than correctness. Object details may be lost when new
    /// data is added to the game. GW2SDK must be updated to get the full representation.</remarks>
    Undefined
}
