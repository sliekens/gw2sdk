using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Items;

namespace GuildWars2;

/// <summary>
/// A factory for creating JSON converters for the Extensible struct.
/// </summary>
public class ExtensibleEnumJsonConverter : JsonConverterFactory
{
    private static readonly Dictionary<Type, JsonConverter> Converters = new();

    static ExtensibleEnumJsonConverter()
    {
        Register<Rarity>();
        Register<GameType>();
        Register<RaceName>();
        Register<ProfessionName>();
        Register<BodyType>();
        Register<DamageType>();
        Register<AttributeName>();
        Register<WeightClass>();
        Register<InfusionSlotUpgradeKind>();
        Register<AttributeName>();

        static void Register<TEnum>() where TEnum : struct, Enum
        {
            Converters[typeof(TEnum)] = new ExtensibleEnumJsonConverterInner<TEnum>();
        }
    }

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType) return false;
        return typeToConvert.GetGenericTypeDefinition() == typeof(Extensible<>);
    }

    /// <inheritdoc />
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var enumType = typeToConvert.GetGenericArguments()[0];
        if (Converters.TryGetValue(enumType, out var converter))
        {
            return converter;
        }
        else
        {
            throw new NotSupportedException($"Enum type {enumType} is not supported");
        }
    }

    /// <summary>
    /// A JSON converter for the Extensible struct with a specific enum type.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    private class ExtensibleEnumJsonConverterInner<TEnum> : JsonConverter<Extensible<TEnum>> where TEnum : struct, Enum
    {
        /// <inheritdoc />
        public override Extensible<TEnum> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var name = reader.GetString();
            return new Extensible<TEnum>(name!);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Extensible<TEnum> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
