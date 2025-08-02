using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.JadeBots;

internal sealed class JadeBotSkinJsonConverter : JsonConverter<JadeBotSkin>
{
    public override JadeBotSkin? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return Read(document.RootElement);
    }

    public static JadeBotSkin? Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            UnlockItemId = json.GetProperty("unlock_item_id").GetInt32()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        JadeBotSkin value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteNumber("unlock_item_id", value.UnlockItemId);
        writer.WriteEndObject();
    }
}
