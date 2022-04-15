using System;
using System.Collections.Generic;
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
                $"The requested operation requires an element of type 'String', but the target element has type '{json.ValueKind}'.");
        }

        return value;
    }

    internal static Dictionary<string, TValue> GetMap<TValue>(this JsonElement json, Func<JsonElement, TValue> convert)
    {
        var values = new Dictionary<string, TValue>();
        foreach (var member in json.EnumerateObject())
        {
            values[member.Name] = convert(member.Value);
        }

        return values;
    }

    internal static IEnumerable<TValue> GetArray<TValue>(
        this JsonElement json,
        Func<JsonElement, TValue> resultSelector
    )
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var item in json.EnumerateArray())
        {
            yield return resultSelector(item);
        }
    }

    internal static IEnumerable<int> GetInt32Array(this JsonElement json)
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var item in json.EnumerateArray())
        {
            yield return item.GetInt32();
        }
    }

    internal static IEnumerable<string> GetStringArray(this JsonElement json)
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var item in json.EnumerateArray())
        {
            yield return GetStringRequired(item);
        }
    }
}
