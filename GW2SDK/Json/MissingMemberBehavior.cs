using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Json
{
    /// <summary>Describes the desired program behavior when a JSON document does not match the CLR type.</summary>
    [PublicAPI]
    [DefaultValue(Undefined)]
    public enum MissingMemberBehavior
    {
        /// <summary>Ignores unexpected JSON properties. Converts unexpected string constants to an enum value with no name.
        /// Replaces unexpected or missing type names with the base class when the type is polymorphic.</summary>
        /// <remarks>Use this when completeness of the data is more important than depth. Object details may be lost when new data is added to the game. GW2SDK must be updated to get the full representation.</remarks>
        Undefined,

        /// <summary>Throws an error for unexpected JSON properties. Throws an error when string contants can't be converted to an
        /// enum. Throws an error for unexpected or missing type names when the type is polymorphic.</summary>
        /// <remarks>Use this when accuracy of the data is more important than breadth. Errors may be thrown when new data is added to the game. GW2SDK must be updated to use the new data.</remarks>
        Error
    }
}
