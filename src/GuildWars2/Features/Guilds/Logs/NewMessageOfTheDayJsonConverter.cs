using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class NewMessageOfTheDayJsonConverter : JsonConverter<NewMessageOfTheDay>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "new_message_of_the_day";

    public override NewMessageOfTheDay Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, NewMessageOfTheDay value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static NewMessageOfTheDay Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement messageOfTheDayJson = json.GetProperty("message_of_the_day");
        return new NewMessageOfTheDay
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            MessageOfTheDay = messageOfTheDayJson.GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, NewMessageOfTheDay value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("message_of_the_day", value.MessageOfTheDay);
        writer.WriteEndObject();
    }
}
