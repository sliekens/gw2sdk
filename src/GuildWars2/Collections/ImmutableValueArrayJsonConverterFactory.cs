using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
        Type bclInterfaceType = typeof(IImmutableList<>).MakeGenericType(elementType);
        Type enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
        ConstructorInfo? ctor = typeToConvert.GetConstructor([enumerableType]);
        return new Converter(bclInterfaceType, ctor!);
    }

    private sealed class Converter(Type bclInterfaceType, ConstructorInfo constructor) : JsonConverter<object>
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
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, bclInterfaceType, options);
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
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            object? list = JsonSerializer.Deserialize(ref reader, bclInterfaceType, options);
            if (list is null)
            {
                return null;
            }

            return constructor.Invoke([list]);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }
    }
}
