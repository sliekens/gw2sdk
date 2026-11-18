using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal sealed class ActionSkillJsonConverter : JsonConverter<ActionSkill>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "action_skill";

    public override ActionSkill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ActionSkill value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ActionSkill Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case Skills.EliteSkillJsonConverter.DiscriminatorValue:
                    return Skills.EliteSkillJsonConverter.Read(json);
                case Skills.HealSkillJsonConverter.DiscriminatorValue:
                    return Skills.HealSkillJsonConverter.Read(json);
                case Skills.UtilitySkillJsonConverter.DiscriminatorValue:
                    return Skills.UtilitySkillJsonConverter.Read(json);
                case Skills.BundleSkillJsonConverter.DiscriminatorValue:
                    return Skills.BundleSkillJsonConverter.Read(json);
                case Skills.LockedSkillJsonConverter.DiscriminatorValue:
                    return Skills.LockedSkillJsonConverter.Read(json);
                case Skills.MonsterSkillJsonConverter.DiscriminatorValue:
                    return Skills.MonsterSkillJsonConverter.Read(json);
                case Skills.PetSkillJsonConverter.DiscriminatorValue:
                    return Skills.PetSkillJsonConverter.Read(json);
                case Skills.ProfessionSkillJsonConverter.DiscriminatorValue:
                    return Skills.ProfessionSkillJsonConverter.Read(json);
                case Skills.ToolbeltSkillJsonConverter.DiscriminatorValue:
                    return Skills.ToolbeltSkillJsonConverter.Read(json);
                case Skills.WeaponSkillJsonConverter.DiscriminatorValue:
                    return Skills.WeaponSkillJsonConverter.Read(json);
                case SlotSkillJsonConverter.DiscriminatorValue:
                    return SlotSkillJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
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
        return new ActionSkill
        {
            Id = idJson.GetInt32(),
            Name = nameJson.GetStringRequired(),
            Facts = factsJson.ValueKind == JsonValueKind.Null ? null : factsJson.GetList(static (in value) => FactJsonConverter.Read(value)),
            TraitedFacts = traitedFactsJson.ValueKind == JsonValueKind.Null ? null : traitedFactsJson.GetList(static (in value) => TraitedFactJsonConverter.Read(value)),
            Description = descriptionJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            ChatLink = chatLinkJson.GetStringRequired(),
            Categories = categoriesJson.GetList(static (in value) => value.GetEnum<SkillCategoryName>()),
            SkillFlags = SkillFlagsJsonConverter.Read(skillFlagsJson),
            WeaponType = weaponTypeJson.GetEnum<WeaponType>(),
            Professions = professionsJson.GetList(static (in value) => value.GetEnum<GuildWars2.Hero.ProfessionName>()),
            Slot = slotJson.GetEnum<SkillSlot>(),
            FlipSkillId = flipSkillIdJson.ValueKind == JsonValueKind.Null ? null : flipSkillIdJson.GetInt32(),
            NextSkillId = nextSkillIdJson.ValueKind == JsonValueKind.Null ? null : nextSkillIdJson.GetInt32(),
            PreviousSkillId = previousSkillIdJson.ValueKind == JsonValueKind.Null ? null : previousSkillIdJson.GetInt32(),
            SpecializationId = specializationIdJson.ValueKind == JsonValueKind.Null ? null : specializationIdJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, ActionSkill value)
    {
        if (value is Skills.EliteSkill eliteSkill)
        {
            Skills.EliteSkillJsonConverter.Write(writer, eliteSkill);
        }
        else if (value is Skills.HealSkill healSkill)
        {
            Skills.HealSkillJsonConverter.Write(writer, healSkill);
        }
        else if (value is Skills.UtilitySkill utilitySkill)
        {
            Skills.UtilitySkillJsonConverter.Write(writer, utilitySkill);
        }
        else if (value is Skills.BundleSkill bundleSkill)
        {
            Skills.BundleSkillJsonConverter.Write(writer, bundleSkill);
        }
        else if (value is Skills.LockedSkill lockedSkill)
        {
            Skills.LockedSkillJsonConverter.Write(writer, lockedSkill);
        }
        else if (value is Skills.MonsterSkill monsterSkill)
        {
            Skills.MonsterSkillJsonConverter.Write(writer, monsterSkill);
        }
        else if (value is Skills.PetSkill petSkill)
        {
            Skills.PetSkillJsonConverter.Write(writer, petSkill);
        }
        else if (value is Skills.ProfessionSkill professionSkill)
        {
            Skills.ProfessionSkillJsonConverter.Write(writer, professionSkill);
        }
        else if (value is Skills.ToolbeltSkill toolbeltSkill)
        {
            Skills.ToolbeltSkillJsonConverter.Write(writer, toolbeltSkill);
        }
        else if (value is Skills.WeaponSkill weaponSkill)
        {
            Skills.WeaponSkillJsonConverter.Write(writer, weaponSkill);
        }
        else if (value is SlotSkill slotSkill)
        {
            SlotSkillJsonConverter.Write(writer, slotSkill);
        }
        else if (value.GetType() == typeof(ActionSkill))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteNumber("id", value.Id);
            writer.WriteString("name", value.Name);
            if (value.Facts is { } presentFacts)
            {
                writer.WriteStartArray("facts");
                foreach (Fact item in presentFacts)
                {
                    FactJsonConverter.Write(writer, item);
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
                foreach (TraitedFact item in presentTraitedFacts)
                {
                    TraitedFactJsonConverter.Write(writer, item);
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
            foreach (GuildWars2.Extensible<SkillCategoryName> item in value.Categories)
            {
                writer.WriteStringValue(item.ToString());
            }
            writer.WriteEndArray();
            writer.WritePropertyName("skill_flags");
            SkillFlagsJsonConverter.Write(writer, value.SkillFlags);
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
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
