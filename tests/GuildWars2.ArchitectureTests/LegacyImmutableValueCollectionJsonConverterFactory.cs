using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Collections;

namespace GuildWars2.ArchitectureTests;

/// <summary>The immutable value collection converters as they were before
/// https://github.com/sliekens/gw2sdk/issues/521 (copied from main), used as the oracle by
/// <see cref="ImmutableValueCollectionJsonMatrixTest" />. Registered in <see cref="JsonSerializerOptions.Converters" />,
/// it takes precedence over the [JsonConverter] attributes on the collection types.</summary>
internal sealed class LegacyImmutableValueCollectionJsonConverterFactory : JsonConverterFactory
{
    private static readonly Dictionary<Type, Type> Converters = new()
    {
        [typeof(IImmutableValueList<>)] = typeof(InterfaceListConverter<>),
        [typeof(ImmutableValueList<>)] = typeof(ListConverter<>),
        [typeof(ImmutableValueArray<>)] = typeof(ArrayConverter<>),
        [typeof(IImmutableValueSet<>)] = typeof(InterfaceSetConverter<>),
        [typeof(ImmutableValueSet<>)] = typeof(SetConverter<>),
        [typeof(IImmutableValueDictionary<,>)] = typeof(InterfaceDictionaryConverter<,>),
        [typeof(ImmutableValueDictionary<,>)] = typeof(DictionaryConverter<,>)
    };

    public override bool CanConvert(Type typeToConvert)
    {
        ArgumentNullException.ThrowIfNull(typeToConvert);
        return typeToConvert.IsGenericType && Converters.ContainsKey(typeToConvert.GetGenericTypeDefinition());
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(typeToConvert);
        Type converterType = Converters[typeToConvert.GetGenericTypeDefinition()];
        return (JsonConverter)Activator.CreateInstance(converterType.MakeGenericType(typeToConvert.GetGenericArguments()))!;
    }

    private sealed class InterfaceListConverter<T> : JsonConverter<IImmutableValueList<T>>
    {
        public override void Write(Utf8JsonWriter writer, IImmutableValueList<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableList<T>>(writer, value, options);
        }

        public override IImmutableValueList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableList<T>? list = JsonSerializer.Deserialize<IImmutableList<T>>(ref reader, options);
            return list is null ? null : [.. list];
        }
    }

    private sealed class ListConverter<T> : JsonConverter<ImmutableValueList<T>>
    {
        public override void Write(Utf8JsonWriter writer, ImmutableValueList<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableList<T>>(writer, value, options);
        }

        public override ImmutableValueList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableList<T>? list = JsonSerializer.Deserialize<IImmutableList<T>>(ref reader, options);
            return list is null ? null : [.. list];
        }
    }

    private sealed class ArrayConverter<T> : JsonConverter<ImmutableValueArray<T>>
    {
        public override void Write(Utf8JsonWriter writer, ImmutableValueArray<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableList<T>>(writer, value, options);
        }

        public override ImmutableValueArray<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableList<T>? list = JsonSerializer.Deserialize<IImmutableList<T>>(ref reader, options);
            return list is null ? null : [.. list];
        }
    }

    private sealed class InterfaceSetConverter<T> : JsonConverter<IImmutableValueSet<T>>
    {
        public override void Write(Utf8JsonWriter writer, IImmutableValueSet<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableSet<T>>(writer, value, options);
        }

        public override IImmutableValueSet<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableSet<T>? set = JsonSerializer.Deserialize<IImmutableSet<T>>(ref reader, options);
            return set is null ? null : [.. set];
        }
    }

    private sealed class SetConverter<T> : JsonConverter<ImmutableValueSet<T>>
    {
        public override void Write(Utf8JsonWriter writer, ImmutableValueSet<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableSet<T>>(writer, value, options);
        }

        public override ImmutableValueSet<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableSet<T>? set = JsonSerializer.Deserialize<IImmutableSet<T>>(ref reader, options);
            return set is null ? null : [.. set];
        }
    }

    private sealed class InterfaceDictionaryConverter<TKey, TValue> : JsonConverter<IImmutableValueDictionary<TKey, TValue>>
        where TKey : notnull
    {
        public override void Write(Utf8JsonWriter writer, IImmutableValueDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableDictionary<TKey, TValue>>(writer, value, options);
        }

        public override IImmutableValueDictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableDictionary<TKey, TValue>? dictionary = JsonSerializer.Deserialize<IImmutableDictionary<TKey, TValue>>(ref reader, options);
            return dictionary is null ? null : new ImmutableValueDictionary<TKey, TValue>(dictionary);
        }
    }

    private sealed class DictionaryConverter<TKey, TValue> : JsonConverter<ImmutableValueDictionary<TKey, TValue>>
        where TKey : notnull
    {
        public override void Write(Utf8JsonWriter writer, ImmutableValueDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<IImmutableDictionary<TKey, TValue>>(writer, value, options);
        }

        public override ImmutableValueDictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IImmutableDictionary<TKey, TValue>? dictionary = JsonSerializer.Deserialize<IImmutableDictionary<TKey, TValue>>(ref reader, options);
            return dictionary is null ? null : new ImmutableValueDictionary<TKey, TValue>(dictionary);
        }
    }
}
