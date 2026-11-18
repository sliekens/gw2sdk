using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class GuildBankActivityJsonConverter : JsonConverter<GuildBankActivity>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_bank_activity";

    public override GuildBankActivity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildBankActivity value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildBankActivity Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement operationJson = json.GetProperty("operation");
        JsonElement itemIdJson = json.GetProperty("item_id");
        JsonElement countJson = json.GetProperty("count");
        JsonElement coinsJson = json.GetProperty("coins");
        return new GuildBankActivity
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            Operation = operationJson.GetEnum<GuildBankOperationKind>(),
            ItemId = itemIdJson.GetInt32(),
            Count = countJson.GetInt32(),
            Coins = new GuildWars2.Coin(coinsJson.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildBankActivity value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteString("operation", value.Operation.ToString());
        writer.WriteNumber("item_id", value.ItemId);
        writer.WriteNumber("count", value.Count);
        writer.WriteNumber("coins", value.Coins.Amount);
        writer.WriteEndObject();
    }
}
