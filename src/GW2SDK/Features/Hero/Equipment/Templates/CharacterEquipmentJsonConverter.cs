using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class CharacterEquipmentJsonConverter : JsonConverter<CharacterEquipment>
{
    public override CharacterEquipment Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        return Read(jsonDocument.RootElement);
    }

    public static CharacterEquipment Read(in JsonElement json)
    {
        return new()
        {
            Items = json.GetProperty("items").GetList(EquipmentItemJsonConverter.Read)
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        CharacterEquipment value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteStartArray("items");
        foreach (EquipmentItem item in value.Items)
        {
            EquipmentItemJsonConverter.Write(writer, item);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
