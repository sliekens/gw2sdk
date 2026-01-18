using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Collections;

/// <summary>JSON converter factory for <see cref="ImmutableValueArray{T}"/>.</summary>
public sealed class ImmutableValueArrayJsonConverterFactory : JsonConverterFactory
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
        return genericTypeDefinition == typeof(ImmutableValueArray<>);
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
        Type elementType = typeToConvert.GetGenericArguments()[0];
        return (JsonConverter)Activator.CreateInstance(typeof(Converter<>).MakeGenericType(elementType))!;
    }

    private sealed class Converter<T> : JsonConverter<ImmutableValueArray<T>>
    {
        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableList<T> serialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableList<T> serialization is supported by System.Text.Json."
        )]
        public override void Write(Utf8JsonWriter writer, ImmutableValueArray<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableList<T>>(writer, value, options);
        }

        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableList<T> deserialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableList<T> deserialization is supported by System.Text.Json."
        )]
        public override ImmutableValueArray<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableList<T>? list = JsonSerializer.Deserialize<IImmutableList<T>>(ref reader, options);
            return list is null ? null : [.. list];
        }
    }
}
