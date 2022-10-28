using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GW2SDK.Json;

internal static class JsonElementExtensions
{
    /// <summary>Returns a string, or throws if the element is null or not a string.</summary>
    /// <param name="json">A String value.</param>
    /// <returns>The value of the JSON element as a non-null string (can be empty).</returns>
    internal static string GetStringRequired(this JsonElement json)
    {
        var value = json.GetString();
        if (value is null)
        {
            throw new InvalidOperationException(
                $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'."
            );
        }

        return value;
    }

    /// <summary>Converts a JSON array to a set.</summary>
    /// <typeparam name="TValue">The type of values in the set.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="resultSelector">A function that converts each item in the array to its destination type.</param>
    /// <returns></returns>
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
        values.UnionWith(json.EnumerateArray().Select(resultSelector));
        return values;
    }

    internal static Dictionary<string, TValue> GetMap<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> resultSelector
    )
    {
        var values = new Dictionary<string, TValue>();
        foreach (var member in json.EnumerateObject())
        {
            values[member.Name] = resultSelector(member.Value);
        }

        return values;
    }

    internal static TEnum GetEnum<TEnum>(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    ) where TEnum : struct, Enum =>
        missingMemberBehavior == MissingMemberBehavior.Error
            ? TryHardParse<TEnum>(json.GetStringRequired())
            : Parse<TEnum>(json.GetStringRequired());

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
