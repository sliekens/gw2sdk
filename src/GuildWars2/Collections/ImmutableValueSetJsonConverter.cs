using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Collections;

/// <summary>JSON converter factory for <see cref="ImmutableValueSet{T}"/>.</summary>
public sealed class ImmutableValueSetJsonConverter : JsonConverterFactory
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
        return genericTypeDefinition == typeof(ImmutableValueSet<>);
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
        Type bclInterfaceType = typeof(IImmutableSet<>).MakeGenericType(elementType);
        return new Converter(bclInterfaceType, typeToConvert);
    }

    private sealed class Converter(Type bclInterfaceType, Type collectionType) : JsonConverter<object>
    {
        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableSet<T> serialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableSet<T> serialization is supported by System.Text.Json."
        )]
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, bclInterfaceType, options);
        }

        [UnconditionalSuppressMessage(
            "AOT",
            "IL3050",
            Justification = "IImmutableSet<T> deserialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026",
            Justification = "IImmutableSet<T> deserialization is supported by System.Text.Json."
        )]
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2067",
            Justification = "The collection type has a constructor accepting IEnumerable<T>."
        )]
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            object? set = JsonSerializer.Deserialize(ref reader, bclInterfaceType, options);
            if (set is null)
            {
                return null;
            }

            return Activator.CreateInstance(collectionType, [set]);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }
    }
}
