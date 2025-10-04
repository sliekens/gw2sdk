using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class PvpEquipmentJsonConverter : JsonConverter<PvpEquipment>
{
    public override PvpEquipment? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static PvpEquipment Read(in JsonElement json)
    {
        return new()
        {
            AmuletId = json.GetProperty("amulet_id").GetNullableInt32(),
            RuneId = json.GetProperty("rune_id").GetNullableInt32(),
            SigilIds = json.GetProperty("sigil_ids")
                .GetList(static (in value) => value.GetNullableInt32())
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        PvpEquipment value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, PvpEquipment value)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("amulet_id");
        if (value.AmuletId.HasValue)
        {
            writer.WriteNumberValue(value.AmuletId.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("rune_id");
        if (value.RuneId.HasValue)
        {
            writer.WriteNumberValue(value.RuneId.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteStartArray("sigil_ids");
        foreach (int? sigilId in value.SigilIds)
        {
            if (sigilId.HasValue)
            {
                writer.WriteNumberValue(sigilId.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
