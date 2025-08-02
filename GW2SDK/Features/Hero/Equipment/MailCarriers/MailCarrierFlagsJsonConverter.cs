using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal sealed class MailCarrierFlagsJsonConverter : JsonConverter<MailCarrierFlags>
{
    public override MailCarrierFlags? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        MailCarrierFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static MailCarrierFlags Read(in JsonElement json)
    {
        return new()
        {
            Default = json.GetProperty("default").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in JsonElement value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, MailCarrierFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("default", value.Default);
        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
