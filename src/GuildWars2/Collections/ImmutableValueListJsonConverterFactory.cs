using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
#if NET
using System.Text.Json.Serialization.Metadata;
#endif

namespace GuildWars2.Collections;

/// <summary>JSON converter factory for <see cref="IImmutableValueList{T}"/>.</summary>
public sealed class ImmutableValueListJsonConverterFactory : JsonConverterFactory
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
        return genericTypeDefinition == typeof(IImmutableValueList<>);
    }

    /// <inheritdoc />
    [UnconditionalSuppressMessage(
        "AOT",
        "IL3050",
        Justification = "Known limitation, see https://github.com/sliekens/gw2sdk/issues/521. JsonConverterAttribute only"
            + " provides a Type, so the closed Converter<> must be created at runtime. That works with the JIT and, under"
            + " Native AOT, for reference-type arguments (shared canonical code). For value-type arguments Native AOT"
            + " throws NotSupportedException unless the instantiation was compiled ahead of time for another reason."
            + " Trimming is unaffected: Converter<> is rooted by typeof and has no annotated generic parameters."
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

    private sealed class Converter<T> : JsonConverter<IImmutableValueList<T>>
    {
#if NET
        private JsonTypeInfo<IImmutableList<T>>? typeInfo;
#endif

        public override void Write(Utf8JsonWriter writer, IImmutableValueList<T> value, JsonSerializerOptions options)
        {
#if NET
            JsonSerializer.Serialize(writer, value, ImmutableValueCollectionJson.GetListTypeInfo(ref typeInfo, options));
#else
            JsonSerializer.Serialize<IImmutableList<T>>(writer, value, options);
#endif
        }

        public override IImmutableValueList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
#if NET
            IImmutableList<T>? list = JsonSerializer.Deserialize(ref reader, ImmutableValueCollectionJson.GetListTypeInfo(ref typeInfo, options));
#else
            IImmutableList<T>? list = JsonSerializer.Deserialize<IImmutableList<T>>(ref reader, options);
#endif
            return list is null ? null : [.. list];
        }
    }
}
