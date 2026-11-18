using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal sealed class GuildConsumableJsonConverter : JsonConverter<GuildConsumable>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_consumable";

    public override GuildConsumable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildConsumable value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildConsumable Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
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
        return new GuildConsumable
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

    public static void Write(Utf8JsonWriter writer, GuildConsumable value)
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
}
