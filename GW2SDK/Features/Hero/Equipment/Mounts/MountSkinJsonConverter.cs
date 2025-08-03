using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal sealed class MountSkinJsonConverter : JsonConverter<MountSkin>
{
    public override MountSkin Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        return Read(jsonDocument.RootElement);
    }

    public static MountSkin Read(in JsonElement root)
    {
        var iconString = root.GetProperty("icon").GetStringRequired();
        return new MountSkin
        {
            Id = root.GetProperty("id").GetInt32(),
            Name = root.GetProperty("name").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots = root.GetProperty("dye_slots").GetList(DyeSlotJsonConverter.Read)!,
            Mount = root.GetProperty("mount").GetStringRequired()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        MountSkin value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("icon", value.IconUrl?.ToString());
        writer.WriteStartArray("dye_slots");
        foreach (DyeSlot dyeSlot in value.DyeSlots)
        {
            DyeSlotJsonConverter.Write(writer, dyeSlot);
        }

        writer.WriteEndArray();
        writer.WriteString("mount", value.Mount.ToString());
        writer.WriteEndObject();
    }
}
