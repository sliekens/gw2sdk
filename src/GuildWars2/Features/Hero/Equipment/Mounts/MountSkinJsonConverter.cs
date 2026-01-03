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
        // Optional for backwards compatibility
        Guid mountId = Guid.Empty;
        if (root.TryGetProperty("mount_id", out JsonElement guid))
        {
            mountId = guid.GetGuid();
        }

        string iconString = root.GetProperty("icon").GetStringRequired();
        return new MountSkin
        {
            Id = root.GetProperty("id").GetInt32(),
            Name = root.GetProperty("name").GetStringRequired(), // Type or member is obsolete
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            DyeSlots = root.GetProperty("dye_slots").GetList(DyeSlotJsonConverter.Read)!,
#pragma warning disable CS0618 // Type or member is obsolete
            Mount = root.GetProperty("mount").GetStringRequired(),
#pragma warning restore CS0618 // Type or member is obsolete
            MountId = mountId
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
#pragma warning disable CS0618 // Type or member is obsolete
        writer.WriteString("mount", value.Mount.ToString());
#pragma warning restore CS0618 // Type or member is obsolete
        writer.WriteString("mount_id", value.MountId.ToString());
        writer.WriteEndObject();
    }
}
