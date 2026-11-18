using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Skills;

internal sealed class ProfessionSkillJsonConverter : JsonConverter<ProfessionSkill>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "profession_skill";

    public override ProfessionSkill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ProfessionSkill value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ProfessionSkill Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement factsJson = json.GetProperty("facts");
        JsonElement traitedFactsJson = json.GetProperty("traited_facts");
        JsonElement descriptionJson = json.GetProperty("description");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement chatLinkJson = json.GetProperty("chat_link");
        JsonElement categoriesJson = json.GetProperty("categories");
        JsonElement skillFlagsJson = json.GetProperty("skill_flags");
        JsonElement weaponTypeJson = json.GetProperty("weapon_type");
        JsonElement professionsJson = json.GetProperty("professions");
        JsonElement slotJson = json.GetProperty("slot");
        JsonElement flipSkillIdJson = json.GetProperty("flip_skill_id");
        JsonElement nextSkillIdJson = json.GetProperty("next_skill_id");
        JsonElement previousSkillIdJson = json.GetProperty("previous_skill_id");
        JsonElement specializationIdJson = json.GetProperty("specialization_id");
        JsonElement attunementJson = json.GetProperty("attunement");
        JsonElement costJson = json.GetProperty("cost");
        JsonElement transformSkillsJson = json.GetProperty("transform_skills");
        return new ProfessionSkill
        {
            Id = idJson.GetInt32(),
            Name = nameJson.GetStringRequired(),
            Facts = factsJson.ValueKind == JsonValueKind.Null ? null : factsJson.GetList(static (in value) => GuildWars2.Hero.Builds.FactJsonConverter.Read(value)),
            TraitedFacts = traitedFactsJson.ValueKind == JsonValueKind.Null ? null : traitedFactsJson.GetList(static (in value) => GuildWars2.Hero.Builds.TraitedFactJsonConverter.Read(value)),
            Description = descriptionJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            ChatLink = chatLinkJson.GetStringRequired(),
            Categories = categoriesJson.GetList(static (in value) => value.GetEnum<GuildWars2.Hero.Builds.SkillCategoryName>()),
            SkillFlags = GuildWars2.Hero.Builds.SkillFlagsJsonConverter.Read(skillFlagsJson),
            WeaponType = weaponTypeJson.GetEnum<GuildWars2.Hero.Builds.WeaponType>(),
            Professions = professionsJson.GetList(static (in value) => value.GetEnum<GuildWars2.Hero.ProfessionName>()),
            Slot = slotJson.GetEnum<GuildWars2.Hero.Builds.SkillSlot>(),
            FlipSkillId = flipSkillIdJson.ValueKind == JsonValueKind.Null ? null : flipSkillIdJson.GetInt32(),
            NextSkillId = nextSkillIdJson.ValueKind == JsonValueKind.Null ? null : nextSkillIdJson.GetInt32(),
            PreviousSkillId = previousSkillIdJson.ValueKind == JsonValueKind.Null ? null : previousSkillIdJson.GetInt32(),
            SpecializationId = specializationIdJson.ValueKind == JsonValueKind.Null ? null : specializationIdJson.GetInt32(),
            Attunement = attunementJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<GuildWars2.Hero.Attunement>?)attunementJson.GetEnum<GuildWars2.Hero.Attunement>(),
            Cost = costJson.ValueKind == JsonValueKind.Null ? null : costJson.GetInt32(),
            TransformSkills = transformSkillsJson.ValueKind == JsonValueKind.Null ? null : transformSkillsJson.GetList(static (in value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, ProfessionSkill value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        if (value.Facts is { } presentFacts)
        {
            writer.WriteStartArray("facts");
            foreach (GuildWars2.Hero.Builds.Fact item in presentFacts)
            {
                GuildWars2.Hero.Builds.FactJsonConverter.Write(writer, item);
            }
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNull("facts");
        }
        if (value.TraitedFacts is { } presentTraitedFacts)
        {
            writer.WriteStartArray("traited_facts");
            foreach (GuildWars2.Hero.Builds.TraitedFact item in presentTraitedFacts)
            {
                GuildWars2.Hero.Builds.TraitedFactJsonConverter.Write(writer, item);
            }
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNull("traited_facts");
        }
        writer.WriteString("description", value.Description);
        if (value.IconUrl is { } presentIconUrl)
        {
            writer.WriteString("icon_url", presentIconUrl.OriginalString);
        }
        else
        {
            writer.WriteNull("icon_url");
        }
        writer.WriteString("chat_link", value.ChatLink);
        writer.WriteStartArray("categories");
        foreach (GuildWars2.Extensible<GuildWars2.Hero.Builds.SkillCategoryName> item in value.Categories)
        {
            writer.WriteStringValue(item.ToString());
        }
        writer.WriteEndArray();
        writer.WritePropertyName("skill_flags");
        GuildWars2.Hero.Builds.SkillFlagsJsonConverter.Write(writer, value.SkillFlags);
        writer.WriteString("weapon_type", value.WeaponType.ToString());
        writer.WriteStartArray("professions");
        foreach (GuildWars2.Extensible<GuildWars2.Hero.ProfessionName> item in value.Professions)
        {
            writer.WriteStringValue(item.ToString());
        }
        writer.WriteEndArray();
        writer.WriteString("slot", value.Slot.ToString());
        if (value.FlipSkillId is { } presentFlipSkillId)
        {
            writer.WriteNumber("flip_skill_id", presentFlipSkillId);
        }
        else
        {
            writer.WriteNull("flip_skill_id");
        }
        if (value.NextSkillId is { } presentNextSkillId)
        {
            writer.WriteNumber("next_skill_id", presentNextSkillId);
        }
        else
        {
            writer.WriteNull("next_skill_id");
        }
        if (value.PreviousSkillId is { } presentPreviousSkillId)
        {
            writer.WriteNumber("previous_skill_id", presentPreviousSkillId);
        }
        else
        {
            writer.WriteNull("previous_skill_id");
        }
        if (value.SpecializationId is { } presentSpecializationId)
        {
            writer.WriteNumber("specialization_id", presentSpecializationId);
        }
        else
        {
            writer.WriteNull("specialization_id");
        }
        if (value.Attunement is { } presentAttunement)
        {
            writer.WriteString("attunement", presentAttunement.ToString());
        }
        else
        {
            writer.WriteNull("attunement");
        }
        if (value.Cost is { } presentCost)
        {
            writer.WriteNumber("cost", presentCost);
        }
        else
        {
            writer.WriteNull("cost");
        }
        if (value.TransformSkills is { } presentTransformSkills)
        {
            writer.WriteStartArray("transform_skills");
            foreach (int item in presentTransformSkills)
            {
                writer.WriteNumberValue(item);
            }
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNull("transform_skills");
        }
        writer.WriteEndObject();
    }
}
