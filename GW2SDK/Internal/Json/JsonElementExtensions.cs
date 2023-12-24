using System.Text.Json;

namespace GuildWars2.Json;

internal static class JsonElementExtensions
{
    internal static int? GetNullableInt32(this JsonElement json) =>
        json.ValueKind == JsonValueKind.Null ? null : json.GetInt32();

    /// <summary>Returns a string, or throws if the element is null or not a string.</summary>
    /// <param name="json">A String value.</param>
    /// <returns>The value of the JSON element as a non-null string (can be empty).</returns>
    internal static string GetStringRequired(this JsonElement json) =>
        json.GetString()
        ?? throw new InvalidOperationException(
            $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'."
        );

    /// <summary>Converts a JSON array to a list.</summary>
    /// <typeparam name="TValue">The type of values in the list.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="resultSelector">A function that converts each item in the array to its destination type.</param>
    /// <returns>A list containing the converted results.</returns>
    internal static List<TValue> GetList<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> resultSelector
    )
    {
        var values = new List<TValue>(json.GetArrayLength());
        var enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(resultSelector(enumerator.Current));
        }

        return values;
    }

    /// <summary>Converts a JSON array to a set.</summary>
    /// <typeparam name="TValue">The type of values in the set.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="resultSelector">A function that converts each item in the array to its destination type.</param>
    /// <returns>A set containing the converted results.</returns>
    internal static HashSet<TValue> GetSet<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> resultSelector
    )
    {
#if NET
        var values = new HashSet<TValue>(json.GetArrayLength());
#else
        var values = new HashSet<TValue>();
#endif
        var enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(resultSelector(enumerator.Current));
        }

        return values;
    }

    internal static Dictionary<string, TValue> GetMap<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> resultSelector
    )
    {
        var values = new Dictionary<string, TValue>();
        var enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[enumerator.Current.Name] = resultSelector(enumerator.Current.Value);
        }

        return values;
    }

    internal static TEnum GetEnum<TEnum>(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        missingMemberBehavior == MissingMemberBehavior.Error
            ? Parse<TEnum>(json.GetStringRequired())
            : TryHardParse<TEnum>(json.GetStringRequired());

    /// <summary>A variation on Enum.TryParse() that tries harder.</summary>
    /// <typeparam name="TEnum">The type of Enum to parse.</typeparam>
    /// <param name="name">The name of a member of the Enum.</param>
    /// <returns>The Enum value of the member with the specified name.</returns>
    private static TEnum TryHardParse<TEnum>(string name) where TEnum : struct, Enum
    {
        if (Enum.TryParse(name, true, out TEnum result))
        {
            return result;
        }

        // When parsing fails, treat the value as a constant where the name is unknown
        // i.e. unique strings receive a unique value
        //      and duplicate strings receive the same value
        // (Using hash code is not perfect because of collissions, but it's good enough.)
        // The actual value should be treated as an opaque value
        return (TEnum)Enum.ToObject(typeof(TEnum), name.GetDeterministicHashCode());
    }

    private static TEnum Parse<TEnum>(string name) where TEnum : struct, Enum
    {
        if (!Enum.TryParse(name, true, out TEnum value))
        {
            throw new InvalidOperationException(Strings.UnexpectedMember(name));
        }

        return value;
    }
}
