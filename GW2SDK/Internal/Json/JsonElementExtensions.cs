using System.Text.Json;
using GuildWars2.Collections;

namespace GuildWars2.Json;

internal static class JsonElementExtensions
{
    internal static T? GetNullable<T>(this JsonElement json, Func<JsonElement, T?> transform)
        where T : class
    {
        return json.ValueKind == JsonValueKind.Null ? null : transform(json);
    }

    internal static int? GetNullableInt32(this JsonElement json)
    {
        return json.ValueKind == JsonValueKind.Null ? null : json.GetInt32();
    }

    /// <summary>Returns a string, or throws if the element is null or not a string.</summary>
    /// <param name="json">A String value.</param>
    /// <returns>The value of the JSON element as a non-null string (can be empty).</returns>
    internal static string GetStringRequired(this JsonElement json)
    {
        return json.GetString()
            ?? throw new InvalidOperationException(
                $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'."
            );
    }

    /// <summary>Converts a JSON array to a list.</summary>
    /// <typeparam name="TValue">The type of values in the list.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>A list containing the converted results.</returns>
    internal static ValueList<TValue> GetList<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
        var values = new ValueList<TValue>(json.GetArrayLength());
        var enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    internal static ValueList<TValue>? GetNullableList<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
        return json.ValueKind == JsonValueKind.Null ? null : json.GetList(transform);
    }

    /// <summary>Converts a JSON array to a set.</summary>
    /// <typeparam name="TValue">The type of values in the set.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>A set containing the converted results.</returns>
    internal static ValueHashSet<TValue> GetSet<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
#if NET
        var values = new ValueHashSet<TValue>(json.GetArrayLength());
#else
        var values = new ValueHashSet<TValue>();
#endif
        var enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    internal static ValueDictionary<string, TValue> GetMap<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
        var values = new ValueDictionary<string, TValue>();
        var enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[enumerator.Current.Name] = transform(enumerator.Current.Value);
        }

        return values;
    }

    internal static ValueDictionary<TKey, TValue> GetMap<TKey, TValue>(
        this JsonElement json,
        Func<string, TKey> keySelector,
        Func<JsonElement, TValue> resultSelector
    ) where TKey : notnull
    {
        var values = new ValueDictionary<TKey, TValue>();
        var enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[keySelector(enumerator.Current.Name)] = resultSelector(enumerator.Current.Value);
        }

        return values;
    }

    internal static Extensible<TEnum> GetEnum<TEnum>(this JsonElement json)
        where TEnum : struct, Enum
    {
        return new Extensible<TEnum>(json.GetStringRequired());
    }

    internal static Extensible<TEnum>? GetNullableEnum<TEnum>(this JsonElement json)
        where TEnum : struct, Enum
    {
        return json.ValueKind switch
        {
            JsonValueKind.Null => null as Extensible<TEnum>?,
            _ => json.GetEnum<TEnum>()
        };
    }
}
