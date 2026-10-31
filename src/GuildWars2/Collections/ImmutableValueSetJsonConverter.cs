using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
#if NET
using System.Text.Json.Serialization.Metadata;
#endif

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

    private sealed class Converter<T> : JsonConverter<ImmutableValueSet<T>>
    {
#if NET
        private JsonTypeInfo<IImmutableSet<T>>? typeInfo;
#endif

        public override void Write(Utf8JsonWriter writer, ImmutableValueSet<T> value, JsonSerializerOptions options)
        {
#if NET
            JsonSerializer.Serialize(writer, value, ImmutableValueCollectionJson.GetSetTypeInfo(ref typeInfo, options));
#else
            JsonSerializer.Serialize<IImmutableSet<T>>(writer, value, options);
#endif
        }

        public override ImmutableValueSet<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
#if NET
            IImmutableSet<T>? set = JsonSerializer.Deserialize(ref reader, ImmutableValueCollectionJson.GetSetTypeInfo(ref typeInfo, options));
#else
            IImmutableSet<T>? set = JsonSerializer.Deserialize<IImmutableSet<T>>(ref reader, options);
#endif
            return set is null ? null : [.. set];
        }
    }
}
