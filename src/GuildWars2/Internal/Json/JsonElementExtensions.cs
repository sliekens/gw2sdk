using System.Text.Json;

using GuildWars2.Collections;

namespace GuildWars2.Json;

internal static class JsonElementExtensions
{
    internal static T? GetNullable<T>(this in JsonElement json, JsonTransformNullable<T> transform)
        where T : class
    {
        return json.ValueKind == JsonValueKind.Null ? null : transform(json);
    }

    internal static int? GetNullableInt32(this in JsonElement json)
    {
        return json.ValueKind == JsonValueKind.Null ? null : json.GetInt32();
    }

    /// <summary>Returns a string, or throws if the element is null or not a string.</summary>
    /// <param name="json">A String value.</param>
    /// <returns>The value of the JSON element as a non-null string (can be empty).</returns>
    /// <exception cref="InvalidOperationException">Thrown if the element is null or not a string.</exception>
    internal static string GetStringRequired(this in JsonElement json)
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
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        ValueList<TValue> values = new(json.GetArrayLength());
        JsonElement.ArrayEnumerator enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    internal static ValueList<TValue>? GetNullableList<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
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
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
#if NET
        ValueHashSet<TValue> values = new(json.GetArrayLength());
#else
        ValueHashSet<TValue> values = [];
#endif
        JsonElement.ArrayEnumerator enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    internal static ValueDictionary<string, TValue> GetMap<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        ValueDictionary<string, TValue> values = [];
        JsonElement.ObjectEnumerator enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[enumerator.Current.Name] = transform(enumerator.Current.Value);
        }

        return values;
    }

    internal static ValueDictionary<TKey, TValue> GetMap<TKey, TValue>(
        this in JsonElement json,
        Func<string, TKey> keySelector,
        JsonTransform<TValue> resultSelector
    ) where TKey : notnull
    {
        ValueDictionary<TKey, TValue> values = [];
        JsonElement.ObjectEnumerator enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[keySelector(enumerator.Current.Name)] = resultSelector(enumerator.Current.Value);
        }

        return values;
    }

    internal static Extensible<TEnum> GetEnum<TEnum>(this in JsonElement json)
        where TEnum : struct, Enum
    {
        return new(json.GetStringRequired());
    }

    internal static Extensible<TEnum>? GetNullableEnum<TEnum>(this in JsonElement json)
        where TEnum : struct, Enum
    {
        return json.ValueKind switch
        {
            JsonValueKind.Null => null as Extensible<TEnum>?,
            JsonValueKind.String => json.GetEnum<TEnum>(),
            JsonValueKind.Undefined or
                JsonValueKind.Object or
                JsonValueKind.Array or
                JsonValueKind.Number or
                JsonValueKind.True or
                JsonValueKind.False => throw new InvalidOperationException(
                $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'."
            ),
            _ => json.GetEnum<TEnum>()
        };
    }
}
