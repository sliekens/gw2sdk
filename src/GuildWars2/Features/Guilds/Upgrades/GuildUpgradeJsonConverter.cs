using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildUpgradeJsonConverter : JsonConverter<GuildUpgrade>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade";

    public override GuildUpgrade Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgrade value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgrade Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case AetheriumUpgradeJsonConverter.DiscriminatorValue:
                    return AetheriumUpgradeJsonConverter.Read(json);
                case GuildBankUpgradeJsonConverter.DiscriminatorValue:
                    return GuildBankUpgradeJsonConverter.Read(json);
                case GuildBoostJsonConverter.DiscriminatorValue:
                    return GuildBoostJsonConverter.Read(json);
                case GuildConsumableJsonConverter.DiscriminatorValue:
                    return GuildConsumableJsonConverter.Read(json);
                case GuildDecorationJsonConverter.DiscriminatorValue:
                    return GuildDecorationJsonConverter.Read(json);
                case GuildHallJsonConverter.DiscriminatorValue:
                    return GuildHallJsonConverter.Read(json);
                case GuildHallExpeditionJsonConverter.DiscriminatorValue:
                    return GuildHallExpeditionJsonConverter.Read(json);
                case GuildInitiativeOfficeJsonConverter.DiscriminatorValue:
                    return GuildInitiativeOfficeJsonConverter.Read(json);
                case GuildUnlockerJsonConverter.DiscriminatorValue:
                    return GuildUnlockerJsonConverter.Read(json);
                case WorkshopRestorationJsonConverter.DiscriminatorValue:
                    return WorkshopRestorationJsonConverter.Read(json);
                case WvwObjectiveUpgradeJsonConverter.DiscriminatorValue:
                    return WvwObjectiveUpgradeJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement nameJson = json.GetProperty("name");
        JsonElement descriptionJson = json.GetProperty("description");
        JsonElement buildTimeJson = json.GetProperty("build_time");
        JsonElement iconUrlJson = json.GetProperty("icon_url");
        JsonElement requiredLevelJson = json.GetProperty("required_level");
        JsonElement experienceJson = json.GetProperty("experience");
        JsonElement prerequisitesJson = json.GetProperty("prerequisites");
        JsonElement costsJson = json.GetProperty("costs");
        return new GuildUpgrade
        {
            Id = idJson.GetInt32(),
            Name = nameJson.GetStringRequired(),
            Description = descriptionJson.GetStringRequired(),
            BuildTime = TimeSpan.FromTicks(buildTimeJson.GetInt64()),
            IconUrl = new Uri(iconUrlJson.GetStringRequired(), UriKind.RelativeOrAbsolute),
            RequiredLevel = requiredLevelJson.GetInt32(),
            Experience = experienceJson.GetInt32(),
            Prerequisites = prerequisitesJson.GetList(static (in value) => value.GetInt32()),
            Costs = costsJson.GetList(static (in value) => GuildUpgradeCostJsonConverter.Read(value))
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgrade value)
    {
        if (value is AetheriumUpgrade aetheriumUpgrade)
        {
            AetheriumUpgradeJsonConverter.Write(writer, aetheriumUpgrade);
        }
        else if (value is GuildBankUpgrade guildBankUpgrade)
        {
            GuildBankUpgradeJsonConverter.Write(writer, guildBankUpgrade);
        }
        else if (value is GuildBoost guildBoost)
        {
            GuildBoostJsonConverter.Write(writer, guildBoost);
        }
        else if (value is GuildConsumable guildConsumable)
        {
            GuildConsumableJsonConverter.Write(writer, guildConsumable);
        }
        else if (value is GuildDecoration guildDecoration)
        {
            GuildDecorationJsonConverter.Write(writer, guildDecoration);
        }
        else if (value is GuildHall guildHall)
        {
            GuildHallJsonConverter.Write(writer, guildHall);
        }
        else if (value is GuildHallExpedition guildHallExpedition)
        {
            GuildHallExpeditionJsonConverter.Write(writer, guildHallExpedition);
        }
        else if (value is GuildInitiativeOffice guildInitiativeOffice)
        {
            GuildInitiativeOfficeJsonConverter.Write(writer, guildInitiativeOffice);
        }
        else if (value is GuildUnlocker guildUnlocker)
        {
            GuildUnlockerJsonConverter.Write(writer, guildUnlocker);
        }
        else if (value is WorkshopRestoration workshopRestoration)
        {
            WorkshopRestorationJsonConverter.Write(writer, workshopRestoration);
        }
        else if (value is WvwObjectiveUpgrade wvwObjectiveUpgrade)
        {
            WvwObjectiveUpgradeJsonConverter.Write(writer, wvwObjectiveUpgrade);
        }
        else if (value.GetType() == typeof(GuildUpgrade))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteNumber("id", value.Id);
            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);
            writer.WriteNumber("build_time", value.BuildTime.Ticks);
            writer.WriteString("icon_url", value.IconUrl.OriginalString);
            writer.WriteNumber("required_level", value.RequiredLevel);
            writer.WriteNumber("experience", value.Experience);
            writer.WriteStartArray("prerequisites");
            foreach (int item in value.Prerequisites)
            {
                writer.WriteNumberValue(item);
            }
            writer.WriteEndArray();
            writer.WriteStartArray("costs");
            foreach (GuildUpgradeCost item in value.Costs)
            {
                GuildUpgradeCostJsonConverter.Write(writer, item);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
