using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ConsumableJsonConverter : JsonConverter<Consumable>
{
    public const string DiscriminatorValue = "consumable";

    public const string DiscriminatorName = "$consumable_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Consumable).IsAssignableFrom(typeToConvert);
    }

    public override Consumable Read(
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
        Consumable value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Consumable Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case AppearanceChangerJsonConverter.DiscriminatorValue:
                    return AppearanceChangerJsonConverter.Read(json);
                case BoozeJsonConverter.DiscriminatorValue:
                    return BoozeJsonConverter.Read(json);
                case ContractNpcJsonConverter.DiscriminatorValue:
                    return ContractNpcJsonConverter.Read(json);
                case UnlockerJsonConverter.DiscriminatorValue:
                    return UnlockerJsonConverter.Read(json);
                case UpgradeExtractorJsonConverter.DiscriminatorValue:
                    return UpgradeExtractorJsonConverter.Read(json);
                case CurrencyJsonConverter.DiscriminatorValue:
                    return CurrencyJsonConverter.Read(json);
                case GenericConsumableJsonConverter.DiscriminatorValue:
                    return GenericConsumableJsonConverter.Read(json);
                case HalloweenConsumableJsonConverter.DiscriminatorValue:
                    return HalloweenConsumableJsonConverter.Read(json);
                case MountLicenseJsonConverter.DiscriminatorValue:
                    return MountLicenseJsonConverter.Read(json);
                case TeleportToFriendJsonConverter.DiscriminatorValue:
                    return TeleportToFriendJsonConverter.Read(json);
                case TransmutationJsonConverter.DiscriminatorValue:
                    return TransmutationJsonConverter.Read(json);
                case UtilityJsonConverter.DiscriminatorValue:
                    return UtilityJsonConverter.Read(json);
                case FoodJsonConverter.DiscriminatorValue:
                    return FoodJsonConverter.Read(json);
                case ServiceJsonConverter.DiscriminatorValue:
                    return ServiceJsonConverter.Read(json);
                case RandomUnlockerJsonConverter.DiscriminatorValue:
                    return RandomUnlockerJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new Consumable
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

    public static void Write(Utf8JsonWriter writer, Consumable value)
    {
        switch (value)
        {
            case AppearanceChanger appearanceChanger:
                AppearanceChangerJsonConverter.Write(writer, appearanceChanger);
                break;
            case Booze booze:
                BoozeJsonConverter.Write(writer, booze);
                break;
            case Unlocker unlocker:
                UnlockerJsonConverter.Write(writer, unlocker);
                break;
            case UpgradeExtractor upgradeExtractor:
                UpgradeExtractorJsonConverter.Write(writer, upgradeExtractor);
                break;
            case ContractNpc contractNpc:
                ContractNpcJsonConverter.Write(writer, contractNpc);
                break;
            case Currency currency:
                CurrencyJsonConverter.Write(writer, currency);
                break;
            case GenericConsumable genericConsumable:
                GenericConsumableJsonConverter.Write(writer, genericConsumable);
                break;
            case HalloweenConsumable halloweenConsumable:
                HalloweenConsumableJsonConverter.Write(writer, halloweenConsumable);
                break;
            case MountLicense mountLicense:
                MountLicenseJsonConverter.Write(writer, mountLicense);
                break;
            case TeleportToFriend teleportToFriend:
                TeleportToFriendJsonConverter.Write(writer, teleportToFriend);
                break;
            case Transmutation transmutation:
                TransmutationJsonConverter.Write(writer, transmutation);
                break;
            case Utility utility:
                UtilityJsonConverter.Write(writer, utility);
                break;
            case Food food:
                FoodJsonConverter.Write(writer, food);
                break;
            case Service service:
                ServiceJsonConverter.Write(writer, service);
                break;
            case RandomUnlocker randomUnlocker:
                RandomUnlockerJsonConverter.Write(writer, randomUnlocker);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                ItemJsonConverter.WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }
}
