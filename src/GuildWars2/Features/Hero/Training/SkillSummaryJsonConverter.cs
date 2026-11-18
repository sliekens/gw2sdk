using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal sealed class SkillSummaryJsonConverter : JsonConverter<SkillSummary>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "skill_summary";

    public override SkillSummary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SkillSummary value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SkillSummary Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case Skills.EliteSkillSummaryJsonConverter.DiscriminatorValue:
                    return Skills.EliteSkillSummaryJsonConverter.Read(json);
                case Skills.HealingSkillSummaryJsonConverter.DiscriminatorValue:
                    return Skills.HealingSkillSummaryJsonConverter.Read(json);
                case Skills.ProfessionSkillSummaryJsonConverter.DiscriminatorValue:
                    return Skills.ProfessionSkillSummaryJsonConverter.Read(json);
                case Skills.UtilitySkillSummaryJsonConverter.DiscriminatorValue:
                    return Skills.UtilitySkillSummaryJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement slotJson = json.GetProperty("slot");
        return new SkillSummary
        {
            Id = idJson.GetInt32(),
            Slot = slotJson.GetEnum<GuildWars2.Hero.Builds.SkillSlot>()
        };
    }

    public static void Write(Utf8JsonWriter writer, SkillSummary value)
    {
        if (value is Skills.EliteSkillSummary eliteSkillSummary)
        {
            Skills.EliteSkillSummaryJsonConverter.Write(writer, eliteSkillSummary);
        }
        else if (value is Skills.HealingSkillSummary healingSkillSummary)
        {
            Skills.HealingSkillSummaryJsonConverter.Write(writer, healingSkillSummary);
        }
        else if (value is Skills.ProfessionSkillSummary professionSkillSummary)
        {
            Skills.ProfessionSkillSummaryJsonConverter.Write(writer, professionSkillSummary);
        }
        else if (value is Skills.UtilitySkillSummary utilitySkillSummary)
        {
            Skills.UtilitySkillSummaryJsonConverter.Write(writer, utilitySkillSummary);
        }
        else if (value.GetType() == typeof(SkillSummary))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteNumber("id", value.Id);
            writer.WriteString("slot", value.Slot.ToString());
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
