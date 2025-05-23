using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Novelties;

internal sealed class NoveltyJsonConverter : JsonConverter<Novelty>
{
    public override Novelty? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static Novelty? Read(JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetStringRequired();

        return new Novelty
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString),
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
        writer.WriteString("icon", value.IconUrl.ToString());
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
