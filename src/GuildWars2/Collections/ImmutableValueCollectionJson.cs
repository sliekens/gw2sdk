#if NET
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace GuildWars2.Collections;

/// <summary>Serializes the immutable value collections as the equivalent System.Collections.Immutable interface
/// (<see cref="IImmutableList{T}" />, <see cref="IImmutableSet{T}" />, <see cref="IImmutableDictionary{TKey, TValue}" />),
/// so the JSON and all serializer behavior (number handling, reference handling, runtime-type dispatch for
/// <see cref="object" />, polymorphism, null handling) are exactly what System.Text.Json does for those interfaces.</summary>
/// <remarks>On .NET the collection metadata is built from the element's <see cref="JsonTypeInfo" /> with
/// <see cref="JsonMetadataServices" />, so only metadata for the element types is required. This works with
/// source-generated <see cref="JsonSerializerContext" /> resolvers (with reflection disabled) as well as with the
/// reflection resolver. See https://github.com/sliekens/gw2sdk/issues/521.</remarks>
internal static class ImmutableValueCollectionJson
{
    public static JsonTypeInfo<IImmutableList<T>> GetListTypeInfo<T>(
        ref JsonTypeInfo<IImmutableList<T>>? cache,
        JsonSerializerOptions options
    )
    {
        JsonTypeInfo<IImmutableList<T>>? typeInfo = cache;
        if (typeInfo is null || !ReferenceEquals(typeInfo.Options, options))
        {
            typeInfo = JsonMetadataServices.CreateImmutableEnumerableInfo<IImmutableList<T>, T>(
                options,
                new JsonCollectionInfoValues<IImmutableList<T>> { ElementInfo = GetElementTypeInfo<T>(options) },
                ImmutableList.CreateRange
            );
            InheritNumberHandling(typeInfo);
            cache = typeInfo;
        }

        return typeInfo;
    }

    public static JsonTypeInfo<IImmutableSet<T>> GetSetTypeInfo<T>(
        ref JsonTypeInfo<IImmutableSet<T>>? cache,
        JsonSerializerOptions options
    )
    {
        JsonTypeInfo<IImmutableSet<T>>? typeInfo = cache;
        if (typeInfo is null || !ReferenceEquals(typeInfo.Options, options))
        {
            typeInfo = JsonMetadataServices.CreateImmutableEnumerableInfo<IImmutableSet<T>, T>(
                options,
                new JsonCollectionInfoValues<IImmutableSet<T>> { ElementInfo = GetElementTypeInfo<T>(options) },
                ImmutableHashSet.CreateRange
            );
            InheritNumberHandling(typeInfo);
            cache = typeInfo;
        }

        return typeInfo;
    }

    public static JsonTypeInfo<IImmutableDictionary<TKey, TValue>> GetDictionaryTypeInfo<TKey, TValue>(
        ref JsonTypeInfo<IImmutableDictionary<TKey, TValue>>? cache,
        JsonSerializerOptions options
    )
        where TKey : notnull
    {
        JsonTypeInfo<IImmutableDictionary<TKey, TValue>>? typeInfo = cache;
        if (typeInfo is null || !ReferenceEquals(typeInfo.Options, options))
        {
            typeInfo = JsonMetadataServices.CreateImmutableDictionaryInfo<IImmutableDictionary<TKey, TValue>, TKey, TValue>(
                options,
                new JsonCollectionInfoValues<IImmutableDictionary<TKey, TValue>>
                {
                    KeyInfo = GetElementTypeInfo<TKey>(options),
                    ElementInfo = GetElementTypeInfo<TValue>(options)
                },
                ImmutableDictionary.CreateRange
            );
            InheritNumberHandling(typeInfo);
            cache = typeInfo;
        }

        return typeInfo;
    }

    /// <summary>JsonCollectionInfoValues.NumberHandling is not nullable and defaults to Strict, which would override
    /// <see cref="JsonSerializerOptions.NumberHandling" />. The reflection resolver leaves it unset (inherit from the
    /// options), so reset it to match.</summary>
    /// <param name="typeInfo">The collection metadata, before it is used.</param>
    private static void InheritNumberHandling(JsonTypeInfo typeInfo)
    {
        typeInfo.NumberHandling = null;
    }

    /// <summary>Gets the metadata for an element (or key) type.</summary>
    /// <typeparam name="T">The element or key type.</typeparam>
    /// <param name="options">The options being used for (de)serialization.</param>
    /// <returns>The metadata for <typeparamref name="T" />.</returns>
    private static JsonTypeInfo<T> GetElementTypeInfo<T>(JsonSerializerOptions options)
    {
        if (options.TryGetTypeInfo(typeof(T), out JsonTypeInfo? typeInfo))
        {
            return (JsonTypeInfo<T>)typeInfo;
        }

        // A source-generated context does not walk into types that have a custom converter (such as these collections),
        // so it may lack metadata for the element type. Consumers can add [JsonSerializable(typeof(T))] for most types,
        // but not for SDK types whose [JsonConverter] is internal (e.g. Coin, Extensible<TEnum>): their generator rejects
        // those with SYSLIB1220. Resolve the converter as System.Text.Json would: a converter registered in the options
        // wins over the type's own [JsonConverter] attribute.
        JsonConverter? converter = options.Converters.FirstOrDefault(static candidate => candidate.CanConvert(typeof(T)));
        if (converter is null && typeof(T).GetCustomAttribute<JsonConverterAttribute>(inherit: false) is { } attribute)
        {
            converter = attribute.ConverterType is { } converterType
                ? (JsonConverter?)Activator.CreateInstance(converterType)
                : attribute.CreateConverter(typeof(T));
        }

        if (converter is JsonConverterFactory factory)
        {
            converter = factory.CreateConverter(typeof(T), options);
        }

        if (converter is JsonConverter<T>)
        {
            return JsonMetadataServices.CreateValueInfo<T>(options, converter);
        }

        // Throws NotSupportedException naming the element type that needs [JsonSerializable]
        return (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));
    }
}
#endif
