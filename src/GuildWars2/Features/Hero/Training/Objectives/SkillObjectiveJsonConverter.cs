using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training.Objectives;

internal sealed class SkillObjectiveJsonConverter : JsonConverter<SkillObjective>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "skill_objective";

    public override SkillObjective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SkillObjective value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SkillObjective Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement costJson = json.GetProperty("cost");
        JsonElement skillIdJson = json.GetProperty("skill_id");
        return new SkillObjective
        {
            Cost = costJson.GetInt32(),
            SkillId = skillIdJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, SkillObjective value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("cost", value.Cost);
        writer.WriteNumber("skill_id", value.SkillId);
        writer.WriteEndObject();
    }
}
