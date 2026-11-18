using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal sealed class TreasuryDepositJsonConverter : JsonConverter<TreasuryDeposit>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "treasury_deposit";

    public override TreasuryDeposit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TreasuryDeposit value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TreasuryDeposit Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        JsonElement userJson = json.GetProperty("user");
        JsonElement itemIdJson = json.GetProperty("item_id");
        JsonElement countJson = json.GetProperty("count");
        return new TreasuryDeposit
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset(),
            User = userJson.GetStringRequired(),
            ItemId = itemIdJson.GetInt32(),
            Count = countJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, TreasuryDeposit value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("time", value.Time);
        writer.WriteString("user", value.User);
        writer.WriteNumber("item_id", value.ItemId);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
