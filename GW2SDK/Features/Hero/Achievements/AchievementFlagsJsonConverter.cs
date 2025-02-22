using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal sealed class AchievementFlagsJsonConverter : JsonConverter<AchievementFlags>
{
    public override AchievementFlags? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        AchievementFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementFlags Read(JsonElement json)
    {
        return new AchievementFlags
        {
            CategoryDisplay = json.GetProperty("category_display").GetBoolean(),
            Daily = json.GetProperty("daily").GetBoolean(),
            Hidden = json.GetProperty("hidden").GetBoolean(),
            IgnoreNearlyComplete = json.GetProperty("ignore_nearly_complete").GetBoolean(),
            MoveToTop = json.GetProperty("move_to_top").GetBoolean(),
            Pvp = json.GetProperty("pvp").GetBoolean(),
            RepairOnLogin = json.GetProperty("repair_on_login").GetBoolean(),
            Repeatable = json.GetProperty("repeatable").GetBoolean(),
            RequiresUnlock = json.GetProperty("requires_unlock").GetBoolean(),
            Permanent = json.GetProperty("permanent").GetBoolean(),
            Weekly = json.GetProperty("weekly").GetBoolean(),
            Other = json.GetProperty("other").GetList(static value => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("category_display", value.CategoryDisplay);
        writer.WriteBoolean("daily", value.Daily);
        writer.WriteBoolean("hidden", value.Hidden);
        writer.WriteBoolean("ignore_nearly_complete", value.IgnoreNearlyComplete);
        writer.WriteBoolean("move_to_top", value.MoveToTop);
        writer.WriteBoolean("pvp", value.Pvp);
        writer.WriteBoolean("repair_on_login", value.RepairOnLogin);
        writer.WriteBoolean("repeatable", value.Repeatable);
        writer.WriteBoolean("requires_unlock", value.RequiresUnlock);
        writer.WriteBoolean("permanent", value.Permanent);
        writer.WriteBoolean("weekly", value.Weekly);
        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
