using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2;

/// <summary>
/// A JSON converter for the Extensible struct with a specific enum type.
/// </summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
internal class ExtensibleEnumJsonConverter<TEnum> : JsonConverter<Extensible<TEnum>> where TEnum : struct, Enum
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

    /// <inheritdoc />
    public override Extensible<TEnum> ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var name = reader.GetString();
        return new Extensible<TEnum>(name!);
    }

    /// <inheritdoc />
    public override void WriteAsPropertyName(Utf8JsonWriter writer, Extensible<TEnum> value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
