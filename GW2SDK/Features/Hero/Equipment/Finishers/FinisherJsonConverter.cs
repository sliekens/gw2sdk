using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal sealed class FinisherJsonConverter : JsonConverter<Finisher>
{
    public override Finisher? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static Finisher? Read(JsonElement json)
    {
        return new Finisher
        {
            Id = json.GetProperty("id").GetInt32(),
            LockedText = json.GetProperty("locked_text").GetStringRequired(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(item => item.GetInt32()),
            Order = json.GetProperty("order").GetInt32(),
            IconHref = json.GetProperty("icon").GetStringRequired(),
            Name = json.GetProperty("name").GetStringRequired()
        };
    }

    public override void Write(Utf8JsonWriter writer, Finisher value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("locked_text", value.LockedText);
        writer.WriteStartArray("unlock_item_ids");
        foreach (var element in value.UnlockItemIds)
        {
            writer.WriteNumberValue(element);
        }

        writer.WriteEndArray();

        writer.WriteNumber("order", value.Order);
        writer.WriteString("icon", value.IconHref);
        writer.WriteString("name", value.Name);
        writer.WriteEndObject();
    }
}
