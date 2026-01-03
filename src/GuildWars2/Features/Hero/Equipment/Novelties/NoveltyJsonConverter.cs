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
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static Novelty? Read(in JsonElement json)
    {
        string iconString = json.GetProperty("icon").GetStringRequired();

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
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(static (in entry) => entry.GetInt32())
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
        foreach (int unlockItemId in value.UnlockItemIds)
        {
            writer.WriteNumberValue(unlockItemId);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
