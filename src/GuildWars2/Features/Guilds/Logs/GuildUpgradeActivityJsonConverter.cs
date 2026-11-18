using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class GuildUpgradeActivityJsonConverter : JsonConverter<GuildUpgradeActivity>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_upgrade_activity";

    public override GuildUpgradeActivity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildUpgradeActivity value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildUpgradeActivity Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement actionJson = json.GetProperty("action");
        JsonElement upgradeIdJson = json.GetProperty("upgrade_id");
        JsonElement recipeIdJson = json.GetProperty("recipe_id");
        JsonElement itemIdJson = json.GetProperty("item_id");
        JsonElement countJson = json.GetProperty("count");
        return new GuildUpgradeActivity
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            Action = actionJson.GetEnum<GuildUpgradeAction>(),
            UpgradeId = upgradeIdJson.GetInt32(),
            RecipeId = recipeIdJson.ValueKind == JsonValueKind.Null ? null : recipeIdJson.GetInt32(),
            ItemId = itemIdJson.ValueKind == JsonValueKind.Null ? null : itemIdJson.GetInt32(),
            Count = countJson.ValueKind == JsonValueKind.Null ? null : countJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildUpgradeActivity value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("action", value.Action.ToString());
        writer.WriteNumber("upgrade_id", value.UpgradeId);
        if (value.RecipeId is { } presentRecipeId)
        {
            writer.WriteNumber("recipe_id", presentRecipeId);
        }
        else
        {
            writer.WriteNull("recipe_id");
        }
        if (value.ItemId is { } presentItemId)
        {
            writer.WriteNumber("item_id", presentItemId);
        }
        else
        {
            writer.WriteNull("item_id");
        }
        if (value.Count is { } presentCount)
        {
            writer.WriteNumber("count", presentCount);
        }
        else
        {
            writer.WriteNull("count");
        }
        writer.WriteEndObject();
    }
}
