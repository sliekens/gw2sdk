using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal sealed class SkillJsonConverter : JsonConverter<Skill>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "skill";

    public override Skill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Skill value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Skill Read(in JsonElement json)
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
                case ActionSkillJsonConverter.DiscriminatorValue:
                    return ActionSkillJsonConverter.Read(json);
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
        return new Skill
        {
            Id = idJson.GetInt32(),
            Name = nameJson.GetStringRequired(),
            Facts = factsJson.ValueKind == JsonValueKind.Null ? null : factsJson.GetList(static (in value) => FactJsonConverter.Read(value)),
            TraitedFacts = traitedFactsJson.ValueKind == JsonValueKind.Null ? null : traitedFactsJson.GetList(static (in value) => TraitedFactJsonConverter.Read(value)),
            Description = descriptionJson.GetStringRequired(),
            IconUrl = iconUrlJson.ValueKind == JsonValueKind.Null ? null : new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            ChatLink = chatLinkJson.GetStringRequired(),
            Categories = categoriesJson.GetList(static (in value) => value.GetEnum<SkillCategoryName>()),
            SkillFlags = SkillFlagsJsonConverter.Read(skillFlagsJson)
        };
    }

    public static void Write(Utf8JsonWriter writer, Skill value)
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
        else if (value is ActionSkill actionSkill)
        {
            ActionSkillJsonConverter.Write(writer, actionSkill);
        }
        else if (value.GetType() == typeof(Skill))
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
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
