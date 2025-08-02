using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class EquipmentTemplateJsonConverter : JsonConverter<EquipmentTemplate>
{
    public override EquipmentTemplate? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static EquipmentTemplate Read(in JsonElement json)
    {
        return new()
        {
            TabNumber = json.GetProperty("tab").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Items = json.GetProperty("equipment").GetList(EquipmentItemJsonConverter.Read),
            PvpEquipment = PvpEquipmentJsonConverter.Read(json.GetProperty("pvp_equipment"))
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        EquipmentTemplate value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();
        writer.WriteNumber("tab", value.TabNumber);
        writer.WriteString("name", value.Name);
        writer.WriteStartArray("equipment");
        foreach (var item in value.Items)
        {
            EquipmentItemJsonConverter.Write(writer, item);
        }

        writer.WriteEndArray();
        writer.WritePropertyName("pvp_equipment");
        PvpEquipmentJsonConverter.Write(writer, value.PvpEquipment);
        writer.WriteEndObject();
    }
}
