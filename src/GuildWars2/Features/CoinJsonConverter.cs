using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2;

internal sealed class CoinJsonConverter : JsonConverter<Coin>
{
    public override Coin Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        int amount = reader.GetInt32();
        return new Coin(amount);
    }

    public override void Write(Utf8JsonWriter writer, Coin value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Amount);
    }
}
