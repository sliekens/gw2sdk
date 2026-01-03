using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ItemJsonConverter : JsonConverter<Item>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "item";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Item).IsAssignableFrom(typeToConvert);
    }

    public override Item Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Item Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case CraftingMaterialJsonConverter.DiscriminatorValue:
                    return CraftingMaterialJsonConverter.Read(json);
                case JadeTechModuleJsonConverter.DiscriminatorValue:
                    return JadeTechModuleJsonConverter.Read(json);
                case PowerCoreJsonConverter.DiscriminatorValue:
                    return PowerCoreJsonConverter.Read(json);
                case BagJsonConverter.DiscriminatorValue:
                    return BagJsonConverter.Read(json);
                case MiniatureJsonConverter.DiscriminatorValue:
                    return MiniatureJsonConverter.Read(json);
                case RelicJsonConverter.DiscriminatorValue:
                    return RelicJsonConverter.Read(json);
                case TrophyJsonConverter.DiscriminatorValue:
                    return TrophyJsonConverter.Read(json);
                case BackpackJsonConverter.DiscriminatorValue:
                    return BackpackJsonConverter.Read(json);
                case SalvageToolJsonConverter.DiscriminatorValue:
                    return SalvageToolJsonConverter.Read(json);
                case ContainerJsonConverter.DiscriminatorValue:
                    return ContainerJsonConverter.Read(json);
                case ArmorJsonConverter.DiscriminatorValue:
                    return ArmorJsonConverter.Read(json);
                case ConsumableJsonConverter.DiscriminatorValue:
                    return ConsumableJsonConverter.Read(json);
                case WeaponJsonConverter.DiscriminatorValue:
                    return WeaponJsonConverter.Read(json);
                case GizmoJsonConverter.DiscriminatorValue:
                    return GizmoJsonConverter.Read(json);
                case TrinketJsonConverter.DiscriminatorValue:
                    return TrinketJsonConverter.Read(json);
                case UpgradeComponentJsonConverter.DiscriminatorValue:
                    return UpgradeComponentJsonConverter.Read(json);
                case GatheringToolJsonConverter.DiscriminatorValue:
                    return GatheringToolJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new Item
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static (in value) => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }

    public static void Write(Utf8JsonWriter writer, Item value)
    {
        switch (value)
        {
            case CraftingMaterial craftingMaterial:
                CraftingMaterialJsonConverter.Write(writer, craftingMaterial);
                break;
            case JadeTechModule jadeTechModule:
                JadeTechModuleJsonConverter.Write(writer, jadeTechModule);
                break;
            case PowerCore powerCore:
                PowerCoreJsonConverter.Write(writer, powerCore);
                break;
            case Bag bag:
                BagJsonConverter.Write(writer, bag);
                break;
            case Miniature miniature:
                MiniatureJsonConverter.Write(writer, miniature);
                break;
            case Relic relic:
                RelicJsonConverter.Write(writer, relic);
                break;
            case Trophy trophy:
                TrophyJsonConverter.Write(writer, trophy);
                break;
            case Backpack backpack:
                BackpackJsonConverter.Write(writer, backpack);
                break;
            case SalvageTool salvageTool:
                SalvageToolJsonConverter.Write(writer, salvageTool);
                break;
            case Container container:
                ContainerJsonConverter.Write(writer, container);
                break;
            case Armor armor:
                ArmorJsonConverter.Write(writer, armor);
                break;
            case Consumable consumable:
                ConsumableJsonConverter.Write(writer, consumable);
                break;
            case Weapon weapon:
                WeaponJsonConverter.Write(writer, weapon);
                break;
            case Gizmo gizmo:
                GizmoJsonConverter.Write(writer, gizmo);
                break;
            case Trinket trinket:
                TrinketJsonConverter.Write(writer, trinket);
                break;
            case UpgradeComponent upgradeComponent:
                UpgradeComponentJsonConverter.Write(writer, upgradeComponent);
                break;
            case GatheringTool gatheringTool:
                GatheringToolJsonConverter.Write(writer, gatheringTool);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Item value)
    {
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteNumber("level", value.Level);
        writer.WriteString("rarity", value.Rarity.ToString());
        writer.WriteNumber("vendor_value", value.VendorValue);
        writer.WriteStartArray("game_types");
        foreach (Extensible<GameType> gameType in value.GameTypes)
        {
            writer.WriteStringValue(gameType.ToString());
        }

        writer.WriteEndArray();
        writer.WritePropertyName("flags");
        ItemFlagsJsonConverter.Write(writer, value.Flags);
        writer.WritePropertyName("restrictions");
        ItemRestrictionJsonConverter.Write(writer, value.Restrictions);
        writer.WriteString("chat_link", value.ChatLink);
        writer.WriteString("icon", value.IconUrl?.ToString());
    }
}
