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
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static MailCarrier? Read(in JsonElement json)
    {
        string iconString = json.GetProperty("icon").GetStringRequired();
        return new MailCarrier
        {
            Id = json.GetProperty("id").GetInt32(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(static (in JsonElement entry) => entry.GetInt32()),
            Order = json.GetProperty("order").GetInt32(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
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
        foreach (int itemId in value.UnlockItemIds)
        {
            writer.WriteNumberValue(itemId);
        }

        writer.WriteEndArray();
        writer.WriteNumber("order", value.Order);
        writer.WriteString("icon", value.IconUrl.ToString());
        writer.WriteString("name", value.Name);
        writer.WritePropertyName("flags");
        MailCarrierFlagsJsonConverter.Write(writer, value.Flags);
        writer.WriteEndObject();
    }
}
