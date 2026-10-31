namespace GuildWars2.Collections;

internal static class ImmutableValueCollectionConverters
{
    /// <summary>Explains why a converter for an immutable value collection could not be created.</summary>
    /// <param name="typeToConvert">The collection type.</param>
    /// <param name="innerException">The exception from creating the converter.</param>
    /// <returns>The exception to throw.</returns>
    public static NotSupportedException NotPrecompiled(Type typeToConvert, NotSupportedException innerException)
    {
        return new NotSupportedException(
            $"Cannot create a JSON converter for '{typeToConvert}'. Under Native AOT, GW2SDK can only serialize immutable"
            + " value collections whose value-type elements (or dictionary keys and values) were compiled ahead of time:"
            + " the combinations that GW2SDK's own types use; elements of type bool, int, long, double, decimal, Guid,"
            + " DateTime, DateTimeOffset, TimeSpan or Coin; dictionaries with int or string keys and values of those"
            + " types; and any reference type. Use a reference type or a supported value type, or a collection type"
            + " that System.Text.Json supports natively. See https://github.com/sliekens/gw2sdk/issues/521.",
            innerException
        );
    }
}
