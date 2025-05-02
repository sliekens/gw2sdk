using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal sealed class MountJsonConverter : JsonConverter<Mount>
{
    public override Mount? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static Mount? Read(JsonElement json)
    {
        return new Mount
        {
            Id = new Extensible<MountName>(json.GetProperty("id").GetStringRequired()),
            Name = json.GetProperty("name").GetStringRequired(),
            DefaultSkinId = json.GetProperty("default_skin_id").GetInt32(),
            SkinIds = json.GetProperty("skin_ids").GetList(entry => entry.GetInt32()),
            Skills = json.GetProperty("skills").GetList(SkillReferenceJsonConverter.Read)
        };
    }

    public override void Write(Utf8JsonWriter writer, Mount value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("name", value.Name);
        writer.WriteNumber("default_skin_id", value.DefaultSkinId);
        writer.WriteStartArray("skin_ids");
        foreach (var skinId in value.SkinIds)
        {
            writer.WriteNumberValue(skinId);
        }

        writer.WriteEndArray();
        writer.WriteStartArray("skills");
        foreach (var skill in value.Skills)
        {
            SkillReferenceJsonConverter.Write(writer, skill);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
