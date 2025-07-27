using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs;

internal sealed class SkiffSkinJsonConverter : JsonConverter<SkiffSkin>
{
    public override SkiffSkin? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static SkiffSkin Read(in JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetStringRequired();
        return new SkiffSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots = json.GetProperty("dye_slots").GetList(DyeSlotJsonConverter.Read)!
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        SkiffSkin value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("icon", value.IconUrl.ToString());
        writer.WriteStartArray("dye_slots");
        foreach (var dyeSlot in value.DyeSlots)
        {
            DyeSlotJsonConverter.Write(writer, dyeSlot);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
