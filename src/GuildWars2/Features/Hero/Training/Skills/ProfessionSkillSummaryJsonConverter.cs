using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal sealed class ProfessionSkillSummaryJsonConverter : JsonConverter<ProfessionSkillSummary>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "profession_skill_summary";

    public override ProfessionSkillSummary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ProfessionSkillSummary value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ProfessionSkillSummary Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement slotJson = json.GetProperty("slot");
        JsonElement sourceJson = json.GetProperty("source");
        JsonElement attunementJson = json.GetProperty("attunement");
        return new ProfessionSkillSummary
        {
            Id = idJson.GetInt32(),
            Slot = slotJson.GetEnum<GuildWars2.Hero.Builds.SkillSlot>(),
            Source = sourceJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<GuildWars2.Hero.ProfessionName>?)sourceJson.GetEnum<GuildWars2.Hero.ProfessionName>(),
            Attunement = attunementJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<GuildWars2.Hero.Attunement>?)attunementJson.GetEnum<GuildWars2.Hero.Attunement>()
        };
    }

    public static void Write(Utf8JsonWriter writer, ProfessionSkillSummary value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("slot", value.Slot.ToString());
        if (value.Source is { } presentSource)
        {
            writer.WriteString("source", presentSource.ToString());
        }
        else
        {
            writer.WriteNull("source");
        }
        if (value.Attunement is { } presentAttunement)
        {
            writer.WriteString("attunement", presentAttunement.ToString());
        }
        else
        {
            writer.WriteNull("attunement");
        }
        writer.WriteEndObject();
    }
}
