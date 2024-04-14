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
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>A list containing the converted results.</returns>
    internal static List<TValue> GetList<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
        var values = new List<TValue>(json.GetArrayLength());
        var enumerator = json.EnumerateArray();
        while (enumerator.MoveNext())
        {
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    /// <summary>Converts a JSON array to a set.</summary>
    /// <typeparam name="TValue">The type of values in the set.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>A set containing the converted results.</returns>
    internal static HashSet<TValue> GetSet<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
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
            values.Add(transform(enumerator.Current));
        }

        return values;
    }

    internal static Dictionary<string, TValue> GetMap<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> transform
    )
    {
        var values = new Dictionary<string, TValue>();
        var enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[enumerator.Current.Name] = transform(enumerator.Current.Value);
        }

        return values;
    }

    internal static Dictionary<TKey, TValue> GetMap<TKey, TValue>(
        this JsonElement json,
        Func<string, TKey> keySelector,
        Func<JsonElement, TValue> resultSelector
    ) where TKey : notnull
    {
        var values = new Dictionary<TKey, TValue>();
        var enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            values[keySelector(enumerator.Current.Name)] = resultSelector(enumerator.Current.Value);
        }

        return values;
    }

    internal static Extensible<TEnum> GetEnum<TEnum>(this JsonElement json)
        where TEnum : struct, Enum =>
        new(json.GetStringRequired());
}
