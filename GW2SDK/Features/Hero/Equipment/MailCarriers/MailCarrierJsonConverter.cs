using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal sealed class MailCarrierJsonConverter : JsonConverter<MailCarrier>
{
    public override MailCarrier? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static MailCarrier? Read(JsonElement json)
    {
        return new MailCarrier
        {
            Id = json.GetProperty("id").GetInt32(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(entry => entry.GetInt32()),
            Order = json.GetProperty("order").GetInt32(),
            IconHref = json.GetProperty("icon").GetStringRequired(),
            Name = json.GetProperty("name").GetStringRequired(),
            Flags = MailCarrierFlagsJsonConverter.Read(json.GetProperty("flags"))
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        MailCarrier value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteStartArray("unlock_item_ids");
        foreach (var itemId in value.UnlockItemIds)
        {
            writer.WriteNumberValue(itemId);
        }

        writer.WriteEndArray();
        writer.WriteNumber("order", value.Order);
        writer.WriteString("icon", value.IconHref);
        writer.WriteString("name", value.Name);
        writer.WritePropertyName("flags");
        MailCarrierFlagsJsonConverter.Write(writer, value.Flags);
        writer.WriteEndObject();
    }
}
