
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class BuffJsonConverter : JsonConverter<Buff>
{
    public override Buff? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Buff value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Buff Read(JsonElement json)
    {
        return new Buff
        {
            SkillId = json.GetProperty("skill_id").GetInt32(),
            Description = json.GetProperty("description").GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, Buff value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("skill_id", value.SkillId);
        writer.WriteString("description", value.Description);
        writer.WriteEndObject();
    }
}
