using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;
using GuildWars2.Hero.Equipment;

namespace GuildWars2.Hero.Inventories;

internal sealed class ItemSlotJsonConverter : JsonConverter<ItemSlot>
{
    public override ItemSlot? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ItemSlot value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ItemSlot Read(JsonElement json)
    {
        return new ItemSlot
        {
            Id = json.GetProperty("id").GetInt32(),
            Count = json.GetProperty("count").GetInt32(),
            Charges = json.GetProperty("charges").GetNullableInt32(),
            SkinId = json.GetProperty("skin_id").GetNullableInt32(),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            SecondarySuffixItemId = json.GetProperty("secondary_suffix_item_id").GetNullableInt32(),
            InfusionItemIds = json.GetProperty("infusion_item_ids").GetList(static value => value.GetInt32()),
            DyeColorIds = json.GetProperty("dye_color_ids").GetList(static value => value.GetInt32()),
            Binding = json.GetProperty("binding").GetEnum<ItemBinding>(),
            BoundTo = json.GetProperty("bound_to").GetStringRequired(),
            Stats = SelectedAttributeCombinationJsonConverter.Read(json.GetProperty("stats"))
        };
    }

    public static void Write(Utf8JsonWriter writer, ItemSlot value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("count", value.Count);
        if (value.Charges.HasValue)
        {
            writer.WriteNumber("charges", value.Charges.Value);
        }
        else
        {
            writer.WriteNull("charges");
        }

        if (value.SkinId.HasValue)
        {
            writer.WriteNumber("skin_id", value.SkinId.Value);
        }
        else
        {
            writer.WriteNull("skin_id");
        }

        if (value.SuffixItemId.HasValue)
        {
            writer.WriteNumber("suffix_item_id", value.SuffixItemId.Value);
        }
        else
        {
            writer.WriteNull("suffix_item_id");
        }

        if (value.SecondarySuffixItemId.HasValue)
        {
            writer.WriteNumber("secondary_suffix_item_id", value.SecondarySuffixItemId.Value);
        }
        else
        {
            writer.WriteNull("secondary_suffix_item_id");
        }

        writer.WritePropertyName("infusion_item_ids");
        writer.WriteStartArray();
        foreach (var infusionItemId in value.InfusionItemIds)
        {
            writer.WriteNumberValue(infusionItemId);
        }
        writer.WriteEndArray();
        writer.WritePropertyName("dye_color_ids");
        writer.WriteStartArray();
        foreach (var dyeColorId in value.DyeColorIds)
        {
            writer.WriteNumberValue(dyeColorId);
        }
        writer.WriteEndArray();
        writer.WriteString("binding", value.Binding.ToString());
        writer.WriteString("bound_to", value.BoundTo);
        if (value.Stats is not null)
        {
            writer.WritePropertyName("stats");
            SelectedAttributeCombinationJsonConverter.Write(writer, value.Stats);
        }
        else
        {
            writer.WriteNull("stats");
        }

        writer.WriteEndObject();
    }
}
