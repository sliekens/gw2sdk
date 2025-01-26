using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal sealed class NoveltyJsonConverter : JsonConverter<Novelty>
{
    public override Novelty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }
    public static Novelty? Read(JsonElement json)
    {
        return new Novelty
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetStringRequired(),
            Slot = json.GetProperty("slot").GetStringRequired(),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(entry => entry.GetInt32())
        };
    }
    public override void Write(Utf8JsonWriter writer, Novelty value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteString("icon", value.IconHref);
        writer.WriteString("slot", value.Slot.ToString());
        writer.WriteStartArray("unlock_item_ids");
        foreach (var unlockItemId in value.UnlockItemIds)
        {
            writer.WriteNumberValue(unlockItemId);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
