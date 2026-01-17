using System.Text.Json;

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

    /// <summary>Converts a JSON array to an immutable list.</summary>
    /// <typeparam name="TValue">The type of values in the list.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>An immutable list containing the converted results.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor call that wraps ImmutableList<T>.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0306", Justification = "Cannot simplify to collection expression.")]
    internal static ImmutableValueList<TValue> GetList<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        ImmutableList<TValue>.Builder builder = ImmutableList.CreateBuilder<TValue>();
        foreach (JsonElement element in json.EnumerateArray())
        {
            builder.Add(transform(element));
        }

        return new ImmutableValueList<TValue>(builder.ToImmutable());
    }

    /// <summary>Converts a JSON array to an immutable list, or returns null if the element is null.</summary>
    /// <typeparam name="TValue">The type of values in the list.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>An immutable list containing the converted results, or null if the element is null.</returns>
    internal static ImmutableValueList<TValue>? GetNullableList<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        return json.ValueKind == JsonValueKind.Null ? null : json.GetList(transform);
    }

    /// <summary>Converts a JSON array to an immutable set.</summary>
    /// <typeparam name="TValue">The type of values in the set.</typeparam>
    /// <param name="json">The array element.</param>
    /// <param name="transform">A function that converts each item in the array to its destination type.</param>
    /// <returns>An immutable set containing the converted results.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028", Justification = "Cannot simplify constructor call that wraps ImmutableHashSet<T>.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0306", Justification = "Cannot simplify to collection expression.")]
    internal static ImmutableValueSet<TValue> GetSet<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        ImmutableHashSet<TValue>.Builder builder = ImmutableHashSet.CreateBuilder<TValue>();
        foreach (JsonElement element in json.EnumerateArray())
        {
            builder.Add(transform(element));
        }

        return new ImmutableValueSet<TValue>(builder.ToImmutable());
    }

    internal static ImmutableValueDictionary<string, TValue> GetMap<TValue>(
        this in JsonElement json,
        JsonTransform<TValue> transform
    )
    {
        ImmutableDictionary<string, TValue>.Builder builder =
            ImmutableDictionary.CreateBuilder<string, TValue>();
        JsonElement.ObjectEnumerator enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            builder[enumerator.Current.Name] = transform(enumerator.Current.Value);
        }

        return new ImmutableValueDictionary<string, TValue>(builder.ToImmutable());
    }

    internal static ImmutableValueDictionary<TKey, TValue> GetMap<TKey, TValue>(
        this in JsonElement json,
        Func<string, TKey> keySelector,
        JsonTransform<TValue> resultSelector
    ) where TKey : notnull
    {
        ImmutableDictionary<TKey, TValue>.Builder builder =
            ImmutableDictionary.CreateBuilder<TKey, TValue>();
        JsonElement.ObjectEnumerator enumerator = json.EnumerateObject();
        while (enumerator.MoveNext())
        {
            builder[keySelector(enumerator.Current.Name)] = resultSelector(enumerator.Current.Value);
        }

        return new ImmutableValueDictionary<TKey, TValue>(builder.ToImmutable());
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
