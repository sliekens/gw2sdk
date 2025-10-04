using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal sealed class AccountAchievementJsonConverter : JsonConverter<AccountAchievement>
{
    public override AccountAchievement Read(
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
        AccountAchievement value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AccountAchievement Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Bits = json.GetProperty("bits").GetNullableList(static (in bit) => bit.GetInt32()),
            Current = json.GetProperty("current").GetInt32(),
            Max = json.GetProperty("max").GetInt32(),
            Done = json.GetProperty("done").GetBoolean(),
            Repeated = json.GetProperty("repeated").GetInt32(),
            Unlocked = json.GetProperty("unlocked").GetBoolean()
        };
    }

    public static void Write(Utf8JsonWriter writer, AccountAchievement value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WritePropertyName("bits");
        if (value.Bits is not null)
        {
            writer.WriteStartArray();
            foreach (int bit in value.Bits)
            {
                writer.WriteNumberValue(bit);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteNumber("current", value.Current);
        writer.WriteNumber("max", value.Max);
        writer.WriteBoolean("done", value.Done);
        writer.WriteNumber("repeated", value.Repeated);
        writer.WriteBoolean("unlocked", value.Unlocked);
        writer.WriteEndObject();
    }
}
