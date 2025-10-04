using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal sealed class EquipmentItemJsonConverter : JsonConverter<EquipmentItem>
{
    public override EquipmentItem? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static EquipmentItem Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Count = json.GetProperty("count").GetNullableInt32(),
            Slot = json.GetProperty("slot").GetNullableEnum<EquipmentSlot>(),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            SecondarySuffixItemId = json.GetProperty("secondary_suffix_item_id").GetNullableInt32(),
            InfusionItemIds =
                json.GetProperty("infusion_item_ids").GetList(static (in entry) => entry.GetInt32()),
            SkinId = json.GetProperty("skin_id").GetNullableInt32(),
            Stats =
                json.GetProperty("stats")
                    .GetNullable(SelectedAttributeCombinationJsonConverter.Read),
            Binding = json.GetProperty("binding").GetEnum<ItemBinding>(),
            BoundTo = json.GetProperty("bound_to").GetStringRequired(),
            Location = json.GetProperty("location").GetEnum<EquipmentLocation>(),
            TemplateNumbers =
                json.GetProperty("template_numbers").GetList(static (in entry) => entry.GetInt32()),
            DyeColorIds = json.GetProperty("dye_color_ids").GetList(static (in entry) => entry.GetInt32())
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        EquipmentItem value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, EquipmentItem value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WritePropertyName("count");
        if (value.Count.HasValue)
        {
            writer.WriteNumberValue(value.Count.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("slot");
        if (value.Slot.HasValue)
        {
            writer.WriteStringValue(value.Slot.Value.ToString());
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("suffix_item_id");
        if (value.SuffixItemId.HasValue)
        {
            writer.WriteNumberValue(value.SuffixItemId.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("secondary_suffix_item_id");
        if (value.SecondarySuffixItemId.HasValue)
        {
            writer.WriteNumberValue(value.SecondarySuffixItemId.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteStartArray("infusion_item_ids");
        foreach (int infusionItemId in value.InfusionItemIds)
        {
            writer.WriteNumberValue(infusionItemId);
        }

        writer.WriteEndArray();
        writer.WritePropertyName("skin_id");
        if (value.SkinId.HasValue)
        {
            writer.WriteNumberValue(value.SkinId.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("stats");
        if (value.Stats is not null)
        {
            SelectedAttributeCombinationJsonConverter.Write(writer, value.Stats);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteString("binding", value.Binding.ToString());
        writer.WriteString("bound_to", value.BoundTo);
        writer.WriteString("location", value.Location.ToString());

        writer.WriteStartArray("template_numbers");
        foreach (int templateNumber in value.TemplateNumbers)
        {
            writer.WriteNumberValue(templateNumber);
        }

        writer.WriteEndArray();

        writer.WriteStartArray("dye_color_ids");
        foreach (int dyeColorId in value.DyeColorIds)
        {
            writer.WriteNumberValue(dyeColorId);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
