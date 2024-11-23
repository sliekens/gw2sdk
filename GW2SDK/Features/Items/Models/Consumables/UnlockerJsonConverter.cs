using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class UnlockerJsonConverter : JsonConverter<Unlocker>
{
    public const string DiscriminatorValue = "unlocker";

    public const string DiscriminatorName = "$unlocker_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Unlocker).IsAssignableFrom(typeToConvert);
    }

    public override Unlocker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Unlocker value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Unlocker Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(ConsumableJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString());
        }

        if (!json.GetProperty(ConsumableJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ConsumableJsonConverter.DiscriminatorName).GetString());
        }

        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case BagSlotExpansionJsonConverter.DiscriminatorValue:
                    return BagSlotExpansionJsonConverter.Read(json);
                case BankTabExpansionJsonConverter.DiscriminatorValue:
                    return BankTabExpansionJsonConverter.Read(json);
                case BuildStorageExpansionJsonConverter.DiscriminatorValue:
                    return BuildStorageExpansionJsonConverter.Read(json);
                case BuildTemplateExpansionJsonConverter.DiscriminatorValue:
                    return BuildTemplateExpansionJsonConverter.Read(json);
                case ContentUnlockerJsonConverter.DiscriminatorValue:
                    return ContentUnlockerJsonConverter.Read(json);
                case DyeJsonConverter.DiscriminatorValue:
                    return DyeJsonConverter.Read(json);
                case EquipmentTemplateExpansionJsonConverter.DiscriminatorValue:
                    return EquipmentTemplateExpansionJsonConverter.Read(json);
                case JadeBotSkinUnlockerJsonConverter.DiscriminatorValue:
                    return JadeBotSkinUnlockerJsonConverter.Read(json);
                case MiniatureUnlockerJsonConverter.DiscriminatorValue:
                    return MiniatureUnlockerJsonConverter.Read(json);
                case MistChampionSkinUnlockerJsonConverter.DiscriminatorValue:
                    return MistChampionSkinUnlockerJsonConverter.Read(json);
                case MountSkinUnlockerJsonConverter.DiscriminatorValue:
                    return MountSkinUnlockerJsonConverter.Read(json);
                case OutfitUnlockerJsonConverter.DiscriminatorValue:
                    return OutfitUnlockerJsonConverter.Read(json);
                case RecipeSheetJsonConverter.DiscriminatorValue:
                    return RecipeSheetJsonConverter.Read(json);
                case SharedInventorySlotJsonConverter.DiscriminatorValue:
                    return SharedInventorySlotJsonConverter.Read(json);
                case StorageExpanderJsonConverter.DiscriminatorValue:
                    return StorageExpanderJsonConverter.Read(json);
                case GliderSkinUnlockerJsonConverter.DiscriminatorValue:
                    return GliderSkinUnlockerJsonConverter.Read(json);
            }
        }

        return new Unlocker
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes = json.GetProperty("game_types").GetList(static value => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetString()
        };
    }

    public static void Write(Utf8JsonWriter writer, Unlocker value)
    {
        switch (value)
        {
            case BagSlotExpansion bagSlotExpansion:
                BagSlotExpansionJsonConverter.Write(writer, bagSlotExpansion);
                break;
            case BankTabExpansion bankTabExpansion:
                BankTabExpansionJsonConverter.Write(writer, bankTabExpansion);
                break;
            case BuildStorageExpansion buildStorageExpansion:
                BuildStorageExpansionJsonConverter.Write(writer, buildStorageExpansion);
                break;
            case BuildTemplateExpansion buildTemplateExpansion:
                BuildTemplateExpansionJsonConverter.Write(writer, buildTemplateExpansion);
                break;
            case ContentUnlocker contentUnlocker:
                ContentUnlockerJsonConverter.Write(writer, contentUnlocker);
                break;
            case Dye dye:
                DyeJsonConverter.Write(writer, dye);
                break;
            case EquipmentTemplateExpansion equipmentTemplateExpansion:
                EquipmentTemplateExpansionJsonConverter.Write(writer, equipmentTemplateExpansion);
                break;
            case JadeBotSkinUnlocker jadeBotSkinUnlocker:
                JadeBotSkinUnlockerJsonConverter.Write(writer, jadeBotSkinUnlocker);
                break;
            case MiniatureUnlocker miniatureUnlocker:
                MiniatureUnlockerJsonConverter.Write(writer, miniatureUnlocker);
                break;
            case MistChampionSkinUnlocker mistChampionSkinUnlocker:
                MistChampionSkinUnlockerJsonConverter.Write(writer, mistChampionSkinUnlocker);
                break;
            case MountSkinUnlocker mountSkinUnlocker:
                MountSkinUnlockerJsonConverter.Write(writer, mountSkinUnlocker);
                break;
            case OutfitUnlocker outfitUnlocker:
                OutfitUnlockerJsonConverter.Write(writer, outfitUnlocker);
                break;
            case RecipeSheet recipeSheet:
                RecipeSheetJsonConverter.Write(writer, recipeSheet);
                break;
            case SharedInventorySlot sharedInventorySlot:
                SharedInventorySlotJsonConverter.Write(writer, sharedInventorySlot);
                break;
            case StorageExpander storageExpander:
                StorageExpanderJsonConverter.Write(writer, storageExpander);
                break;
            case GliderSkinUnlocker gliderSkinUnlocker:
                GliderSkinUnlockerJsonConverter.Write(writer, gliderSkinUnlocker);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, ConsumableJsonConverter.DiscriminatorValue);
                writer.WriteString(ConsumableJsonConverter.DiscriminatorName, DiscriminatorValue);
                ItemJsonConverter.WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }
}
