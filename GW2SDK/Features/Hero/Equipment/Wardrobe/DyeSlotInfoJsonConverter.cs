using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class DyeSlotInfoJsonConverter : JsonConverter<DyeSlotInfo>
{
    public override DyeSlotInfo Read(
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
        DyeSlotInfo value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static DyeSlotInfo Read(in JsonElement json)
    {
        return new()
        {
            Default = json.GetProperty("default").GetList(DyeSlotJsonConverter.Read),
            AsuraFemale =
                json.GetProperty("asura_female").GetNullableList(DyeSlotJsonConverter.Read),
            AsuraMale =
                json.GetProperty("asura_male").GetNullableList(DyeSlotJsonConverter.Read),
            CharrFemale =
                json.GetProperty("charr_female").GetNullableList(DyeSlotJsonConverter.Read),
            CharrMale =
                json.GetProperty("charr_male").GetNullableList(DyeSlotJsonConverter.Read),
            HumanFemale =
                json.GetProperty("human_female").GetNullableList(DyeSlotJsonConverter.Read),
            HumanMale =
                json.GetProperty("human_male").GetNullableList(DyeSlotJsonConverter.Read),
            NornFemale =
                json.GetProperty("norn_female").GetNullableList(DyeSlotJsonConverter.Read),
            NornMale = json.GetProperty("norn_male").GetNullableList(DyeSlotJsonConverter.Read),
            SylvariFemale =
                json.GetProperty("sylvari_female").GetNullableList(DyeSlotJsonConverter.Read),
            SylvariMale = json.GetProperty("sylvari_male")
                .GetNullableList(DyeSlotJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, DyeSlotInfo value)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("default");
        foreach (DyeSlot? slot in value.Default)
        {
            if (slot is not null)
            {
                DyeSlotJsonConverter.Write(writer, slot);
            }
            else
            {
                writer.WriteNullValue();
            }
        }

        writer.WriteEndArray();

        WriteNullableList(writer, "asura_female", value.AsuraFemale);
        WriteNullableList(writer, "asura_male", value.AsuraMale);
        WriteNullableList(writer, "charr_female", value.CharrFemale);
        WriteNullableList(writer, "charr_male", value.CharrMale);
        WriteNullableList(writer, "human_female", value.HumanFemale);
        WriteNullableList(writer, "human_male", value.HumanMale);
        WriteNullableList(writer, "norn_female", value.NornFemale);
        WriteNullableList(writer, "norn_male", value.NornMale);
        WriteNullableList(writer, "sylvari_female", value.SylvariFemale);
        WriteNullableList(writer, "sylvari_male", value.SylvariMale);

        writer.WriteEndObject();
    }

    private static void WriteNullableList(
        Utf8JsonWriter writer,
        string propertyName,
        IReadOnlyList<DyeSlot?>? list
    )
    {
        writer.WritePropertyName(propertyName);
        if (list is not null)
        {
            writer.WriteStartArray();
            foreach (DyeSlot? slot in list)
            {
                if (slot is not null)
                {
                    DyeSlotJsonConverter.Write(writer, slot);
                }
                else
                {
                    writer.WriteNullValue();
                }
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
