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
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
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

    public static AchievementFlags Read(in JsonElement json)
    {
        bool categoryDisplay = false;
        bool daily = false;
        bool hidden = false;
        bool ignoreNearlyComplete = false;
        bool moveToTop = false;
        bool pvp = false;
        bool repairOnLogin = false;
        bool repeatable = false;
        bool requiresUnlock = false;
        bool permanent = false;
        bool weekly = false;
        bool monthly = false;
        List<string> other = [];

        foreach (JsonProperty property in json.EnumerateObject())
        {
            if (property.NameEquals("category_display"))
            {
                categoryDisplay = property.Value.GetBoolean();
            }
            else if (property.NameEquals("daily"))
            {
                daily = property.Value.GetBoolean();
            }
            else if (property.NameEquals("hidden"))
            {
                hidden = property.Value.GetBoolean();
            }
            else if (property.NameEquals("ignore_nearly_complete"))
            {
                ignoreNearlyComplete = property.Value.GetBoolean();
            }
            else if (property.NameEquals("move_to_top"))
            {
                moveToTop = property.Value.GetBoolean();
            }
            else if (property.NameEquals("pvp"))
            {
                pvp = property.Value.GetBoolean();
            }
            else if (property.NameEquals("repair_on_login"))
            {
                repairOnLogin = property.Value.GetBoolean();
            }
            else if (property.NameEquals("repeatable"))
            {
                repeatable = property.Value.GetBoolean();
            }
            else if (property.NameEquals("requires_unlock"))
            {
                requiresUnlock = property.Value.GetBoolean();
            }
            else if (property.NameEquals("permanent"))
            {
                permanent = property.Value.GetBoolean();
            }
            else if (property.NameEquals("weekly"))
            {
                weekly = property.Value.GetBoolean();
            }
            else if (property.NameEquals("monthly"))
            {
                monthly = property.Value.GetBoolean();
            }
            else if (property.NameEquals("other"))
            {
                other = property.Value.GetList(static (in value) => value.GetStringRequired());
            }
        }

        return new AchievementFlags
        {
            CategoryDisplay = categoryDisplay,
            Daily = daily,
            Hidden = hidden,
            IgnoreNearlyComplete = ignoreNearlyComplete,
            MoveToTop = moveToTop,
            Pvp = pvp,
            RepairOnLogin = repairOnLogin,
            Repeatable = repeatable,
            RequiresUnlock = requiresUnlock,
            Permanent = permanent,
            Weekly = weekly,
            Monthly = monthly,
            Other = other
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
        writer.WriteBoolean("monthly", value.Monthly);
        writer.WriteStartArray("other");
        foreach (string other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
