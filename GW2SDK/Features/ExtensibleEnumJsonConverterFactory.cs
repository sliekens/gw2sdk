using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2;

internal partial class ExtensibleEnumJsonConverterFactory : JsonConverterFactory
{
    private static readonly Dictionary<Type, JsonConverter> Converters = [];

    static ExtensibleEnumJsonConverterFactory()
    {
        RegisterEnums();
    }

    private static partial void RegisterEnums();

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
        if (!Converters.TryGetValue(enumType, out var converter))
        {
            throw new NotSupportedException($"Enum type {enumType} is not supported");
        }

        return converter;
    }
}
