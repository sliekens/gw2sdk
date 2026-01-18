using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Collections;

/// <summary>JSON converter factory for <see cref="IImmutableValueDictionary{TKey, TValue}"/>.</summary>
public sealed class ImmutableValueDictionaryJsonConverterFactory : JsonConverterFactory
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
        return genericTypeDefinition == typeof(IImmutableValueDictionary<,>);
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
        return (JsonConverter)Activator.CreateInstance(typeof(Converter<,>).MakeGenericType(keyType, valueType))!;
    }

    private sealed class Converter<TKey, TValue> : JsonConverter<IImmutableValueDictionary<TKey, TValue>>
        where TKey : notnull
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
        public override void Write(Utf8JsonWriter writer, IImmutableValueDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableDictionary<TKey, TValue>>(writer, value, options);
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
        public override IImmutableValueDictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableDictionary<TKey, TValue>? dictionary = JsonSerializer.Deserialize<IImmutableDictionary<TKey, TValue>>(ref reader, options);
            return dictionary is null ? null : new ImmutableValueDictionary<TKey, TValue>(dictionary);
        }
    }
}
