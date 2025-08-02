using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal sealed class SkillReferenceJsonConverter : JsonConverter<SkillReference>
{
    public override SkillReference? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static SkillReference Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Slot = json.GetProperty("slot").GetStringRequired()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        SkillReference value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, SkillReference value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("slot", value.Slot.ToString());
        writer.WriteEndObject();
    }
}
