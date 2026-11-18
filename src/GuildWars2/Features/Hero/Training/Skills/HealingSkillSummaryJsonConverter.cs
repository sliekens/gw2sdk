using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal sealed class HealingSkillSummaryJsonConverter : JsonConverter<HealingSkillSummary>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "healing_skill_summary";

    public override HealingSkillSummary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, HealingSkillSummary value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static HealingSkillSummary Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement slotJson = json.GetProperty("slot");
        return new HealingSkillSummary
        {
            Id = idJson.GetInt32(),
            Slot = slotJson.GetEnum<GuildWars2.Hero.Builds.SkillSlot>()
        };
    }

    public static void Write(Utf8JsonWriter writer, HealingSkillSummary value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("slot", value.Slot.ToString());
        writer.WriteEndObject();
    }
}
