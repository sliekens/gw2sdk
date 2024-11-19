
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal sealed class InventoryJsonConverter : JsonConverter<Inventory>
{
    public override Inventory? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Inventory value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Inventory Read(JsonElement json)
    {
        return new Inventory
        {
            Items = json.GetProperty("items").GetList(static value => value.GetNullable(ItemSlotJsonConverter.Read))
        };
    }

    public static void Write(Utf8JsonWriter writer, Inventory value)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("items");
        writer.WriteStartArray();
        foreach (var item in value.Items)
        {
            if (item is null)
            {
                writer.WriteNullValue();
                continue;
            }

            ItemSlotJsonConverter.Write(writer, item);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
