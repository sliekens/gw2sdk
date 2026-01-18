using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Collections;

/// <summary>JSON converter factory for <see cref="ImmutableValueDictionary{TKey, TValue}"/>.</summary>
public sealed class ImmutableValueDictionaryJsonConverter : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        ThrowHelper.ThrowIfNull(typeToConvert);
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        Type genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();
        return genericTypeDefinition == typeof(ImmutableValueDictionary<,>);
    }

    /// <inheritdoc />
    [UnconditionalSuppressMessage(
        "AOT",
        "IL3050",
        Justification = "The generic instantiation exists because the call site has a statically-typed member."
    )]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2026",
        Justification = "The generic instantiation exists because the call site has a statically-typed member."
    )]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2055",
        Justification = "The generic instantiation exists because the call site has a statically-typed member."
    )]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2070",
        Justification = "The generic instantiation exists because the call site has a statically-typed member."
    )]
    public override JsonConverter CreateConverter(
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        ThrowHelper.ThrowIfNull(typeToConvert);
        Type[] typeArguments = typeToConvert.GetGenericArguments();
        Type keyType = typeArguments[0];
        Type valueType = typeArguments[1];
        Type bclInterfaceType = typeof(IImmutableDictionary<,>).MakeGenericType(keyType, valueType);
        return new Converter(bclInterfaceType, typeToConvert);
    }

    private sealed class Converter(Type bclInterfaceType, Type collectionType) : JsonConverter<object>
    {
        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableDictionary<TKey,TValue> serialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableDictionary<TKey,TValue> serialization is supported by System.Text.Json."
        )]
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, bclInterfaceType, options);
        }

        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableDictionary<TKey,TValue> deserialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableDictionary<TKey,TValue> deserialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2067",
            Justification = "The collection type has a constructor accepting IEnumerable<KeyValuePair<TKey,TValue>>."
        )]
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            object? dictionary = JsonSerializer.Deserialize(ref reader, bclInterfaceType, options);
            if (dictionary is null)
            {
                return null;
            }

            return Activator.CreateInstance(collectionType, [dictionary]);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }
    }
}
