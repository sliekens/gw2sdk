using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Outfits;

internal sealed class OutfitJsonConverter : JsonConverter<Outfit>
{
    public override Outfit Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        return Read(jsonDocument.RootElement);
    }

    public static Outfit Read(in JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetStringRequired();
        return new Outfit
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            UnlockItemIds = json.GetProperty("unlock_item_ids").GetList(static (in JsonElement entry) => entry.GetInt32())
        };
    }

    public override void Write(Utf8JsonWriter writer, Outfit value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("icon", value.IconUrl.ToString());
        writer.WriteStartArray("unlock_item_ids");
        foreach (var unlockItemId in value.UnlockItemIds)
        {
            writer.WriteNumberValue(unlockItemId);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
