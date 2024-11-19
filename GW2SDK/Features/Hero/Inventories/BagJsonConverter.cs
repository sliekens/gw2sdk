
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Inventories;

internal sealed class BagJsonConverter : JsonConverter<Bag>
{
    public override Bag? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Bag value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Bag Read(JsonElement json)
    {
        return new Bag
        {
            Id = json.GetProperty("id").GetInt32(),
            Size = json.GetProperty("size").GetInt32(),
            Inventory = InventoryJsonConverter.Read(json.GetProperty("inventory"))
        };
    }

    public static void Write(Utf8JsonWriter writer, Bag value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("size", value.Size);
        writer.WritePropertyName("inventory");
        InventoryJsonConverter.Write(writer, value.Inventory);
        writer.WriteEndObject();
    }
}
