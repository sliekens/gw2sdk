using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Miniatures;

internal sealed class MiniatureJsonConverter : JsonConverter<Miniature>
{
    public override Miniature? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static Miniature? Read(in JsonElement json)
    {
        string iconString = json.GetProperty("icon").GetStringRequired();
        return new Miniature
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            LockedText = json.GetProperty("locked_text").GetStringRequired(),
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Order = json.GetProperty("order").GetInt32(),
            ItemId = json.GetProperty("item_id").GetInt32()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Miniature value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("locked_text", value.LockedText);
        writer.WriteString("icon", value.IconUrl.ToString());
        writer.WriteNumber("order", value.Order);
        writer.WriteNumber("item_id", value.ItemId);
        writer.WriteEndObject();
    }
}
