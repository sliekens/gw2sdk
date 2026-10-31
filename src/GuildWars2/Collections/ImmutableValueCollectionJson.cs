using System.Text.Json;
using System.Text.Json.Serialization;
#if NET
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
#endif

namespace GuildWars2.Collections;

/// <summary>Reads and writes the JSON representation of the immutable value collections by delegating each element (and
/// dictionary key) to its own converter. Only metadata for the element types is required, so this works with
/// source-generated <see cref="JsonSerializerContext" /> resolvers as well as with the reflection resolver.</summary>
internal static class ImmutableValueCollectionJson
{
    public static JsonConverter<T> GetConverter<T>(JsonSerializerOptions options)
    {
#if NET
        // TryGetTypeInfo is trim/AOT-safe: it resolves metadata from the configured resolver
        // (e.g. a source-generated context) instead of falling back to reflection like GetConverter
        if (options.TryGetTypeInfo(typeof(T), out JsonTypeInfo? typeInfo))
        {
            return (JsonConverter<T>)typeInfo.Converter;
        }

        // A source-generated context does not walk into types that have a custom converter (such as these collections),
        // so it may lack metadata for the element type. Consumers can add [JsonSerializable(typeof(T))] for most types,
        // but not for SDK types whose [JsonConverter] is internal (e.g. Coin, Extensible<TEnum>): their generator rejects
        // those with SYSLIB1220. Honor the element type's own [JsonConverter] attribute, as System.Text.Json would.
        if (typeof(T).GetCustomAttribute<JsonConverterAttribute>(inherit: false) is { } attribute)
        {
            JsonConverter? converter = attribute.ConverterType is { } converterType
                ? (JsonConverter?)Activator.CreateInstance(converterType)
                : attribute.CreateConverter(typeof(T));
            if (converter is JsonConverterFactory factory)
            {
                converter = factory.CreateConverter(typeof(T), options);
            }

            if (converter is JsonConverter<T> typed)
            {
                return typed;
            }
        }

        // Throws NotSupportedException naming the type that needs [JsonSerializable]
        return (JsonConverter<T>)options.GetTypeInfo(typeof(T)).Converter;
#else
        // JsonSerializerOptions.GetTypeInfo does not exist in System.Text.Json 6.0
        return (JsonConverter<T>)options.GetConverter(typeof(T));
#endif
    }

    public static void WriteArray<T>(
        Utf8JsonWriter writer,
        IEnumerable<T> values,
        JsonSerializerOptions options
    )
    {
        JsonConverter<T> converter = GetConverter<T>(options);
        writer.WriteStartArray();
        foreach (T value in values)
        {
            WriteValue(writer, value, converter, options);
        }

        writer.WriteEndArray();
    }

    public static ImmutableArray<T> ReadArray<T>(
        ref Utf8JsonReader reader,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException(
                $"Expected {JsonTokenType.StartArray} but got {reader.TokenType}."
            );
        }

        JsonConverter<T> converter = GetConverter<T>(options);
        ImmutableArray<T>.Builder builder = ImmutableArray.CreateBuilder<T>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return builder.ToImmutable();
            }

            builder.Add(ReadValue(ref reader, converter, options));
        }

        throw new JsonException("Unexpected end of JSON input.");
    }

    public static void WriteObject<TKey, TValue>(
        Utf8JsonWriter writer,
        IEnumerable<KeyValuePair<TKey, TValue>> pairs,
        JsonSerializerOptions options
    )
        where TKey : notnull
    {
        JsonConverter<TKey> keyConverter = GetConverter<TKey>(options);
        JsonConverter<TValue> valueConverter = GetConverter<TValue>(options);
        writer.WriteStartObject();
        foreach (KeyValuePair<TKey, TValue> pair in pairs)
        {
            keyConverter.WriteAsPropertyName(writer, pair.Key, options);
            WriteValue(writer, pair.Value, valueConverter, options);
        }

        writer.WriteEndObject();
    }

    public static ImmutableDictionary<TKey, TValue> ReadObject<TKey, TValue>(
        ref Utf8JsonReader reader,
        JsonSerializerOptions options
    )
        where TKey : notnull
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException(
                $"Expected {JsonTokenType.StartObject} but got {reader.TokenType}."
            );
        }

        JsonConverter<TKey> keyConverter = GetConverter<TKey>(options);
        JsonConverter<TValue> valueConverter = GetConverter<TValue>(options);
        ImmutableDictionary<TKey, TValue>.Builder builder =
            ImmutableDictionary.CreateBuilder<TKey, TValue>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return builder.ToImmutable();
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException(
                    $"Expected {JsonTokenType.PropertyName} but got {reader.TokenType}."
                );
            }

            TKey key = keyConverter.ReadAsPropertyName(ref reader, typeof(TKey), options);
            if (!reader.Read())
            {
                break;
            }

            // Last value wins for duplicate keys, matching System.Text.Json's dictionary semantics
            builder[key] = ReadValue(ref reader, valueConverter, options);
        }

        throw new JsonException("Unexpected end of JSON input.");
    }

    private static void WriteValue<T>(
        Utf8JsonWriter writer,
        T value,
        JsonConverter<T> converter,
        JsonSerializerOptions options
    )
    {
        if (value is null && !converter.HandleNull)
        {
            writer.WriteNullValue();
        }
        else
        {
            converter.Write(writer, value, options);
        }
    }

    private static T ReadValue<T>(
        ref Utf8JsonReader reader,
        JsonConverter<T> converter,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType == JsonTokenType.Null && !converter.HandleNull)
        {
            // Same as System.Text.Json: null is the default for reference types and Nullable<T>,
            // but cannot be converted to a non-nullable value type
            if (default(T) is not null)
            {
                throw new JsonException($"The JSON value could not be converted to {typeof(T)}.");
            }

            return default!;
        }

        return converter.Read(ref reader, typeof(T), options)!;
    }
}
