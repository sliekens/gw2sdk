using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
#if NET
using System.Text.Json.Serialization.Metadata;
#endif

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
        Type[] typeArguments = typeToConvert.GetGenericArguments();
        Type keyType = typeArguments[0];
        Type valueType = typeArguments[1];
        return (JsonConverter)Activator.CreateInstance(typeof(Converter<,>).MakeGenericType(keyType, valueType))!;
    }

    private sealed class Converter<TKey, TValue> : JsonConverter<ImmutableValueDictionary<TKey, TValue>>
        where TKey : notnull
    {
#if NET
        private JsonTypeInfo<IImmutableDictionary<TKey, TValue>>? typeInfo;
#endif

        public override void Write(Utf8JsonWriter writer, ImmutableValueDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
#if NET
            JsonSerializer.Serialize(writer, value, ImmutableValueCollectionJson.GetDictionaryTypeInfo(ref typeInfo, options));
#else
            JsonSerializer.Serialize<IImmutableDictionary<TKey, TValue>>(writer, value, options);
#endif
        }

        public override ImmutableValueDictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
#if NET
            IImmutableDictionary<TKey, TValue>? dictionary = JsonSerializer.Deserialize(ref reader, ImmutableValueCollectionJson.GetDictionaryTypeInfo(ref typeInfo, options));
#else
            IImmutableDictionary<TKey, TValue>? dictionary = JsonSerializer.Deserialize<IImmutableDictionary<TKey, TValue>>(ref reader, options);
#endif
#pragma warning disable IDE0028 // Cannot simplify constructor calls that wrap ImmutableDictionary<TKey, TValue>.
            return dictionary is null ? null : new ImmutableValueDictionary<TKey, TValue>(dictionary);
#pragma warning restore IDE0028
        }
    }
}
