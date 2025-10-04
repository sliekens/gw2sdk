using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal sealed class WeaponSkinJsonConverter : JsonConverter<WeaponSkin>
{
    public const string DiscriminatorValue = "weapon_skin";

    public const string DiscriminatorName = "$weapon_skin_type";

    public override WeaponSkin Read(
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
        WeaponSkin value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static WeaponSkin Read(in JsonElement json)
    {
        if (!json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(EquipmentSkinJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case AxeSkinJsonConverter.DiscriminatorValue:
                    return AxeSkinJsonConverter.Read(json);
                case SmallBundleSkinJsonConverter.DiscriminatorValue:
                    return SmallBundleSkinJsonConverter.Read(json);
                case HammerSkinJsonConverter.DiscriminatorValue:
                    return HammerSkinJsonConverter.Read(json);
                case StaffSkinJsonConverter.DiscriminatorValue:
                    return StaffSkinJsonConverter.Read(json);
                case LargeBundleSkinJsonConverter.DiscriminatorValue:
                    return LargeBundleSkinJsonConverter.Read(json);
                case TridentSkinJsonConverter.DiscriminatorValue:
                    return TridentSkinJsonConverter.Read(json);
                case WarhornSkinJsonConverter.DiscriminatorValue:
                    return WarhornSkinJsonConverter.Read(json);
                case ToyTwoHandedSkinJsonConverter.DiscriminatorValue:
                    return ToyTwoHandedSkinJsonConverter.Read(json);
                case ScepterSkinJsonConverter.DiscriminatorValue:
                    return ScepterSkinJsonConverter.Read(json);
                case DaggerSkinJsonConverter.DiscriminatorValue:
                    return DaggerSkinJsonConverter.Read(json);
                case FocusSkinJsonConverter.DiscriminatorValue:
                    return FocusSkinJsonConverter.Read(json);
                case ShortbowSkinJsonConverter.DiscriminatorValue:
                    return ShortbowSkinJsonConverter.Read(json);
                case LongbowSkinJsonConverter.DiscriminatorValue:
                    return LongbowSkinJsonConverter.Read(json);
                case TorchSkinJsonConverter.DiscriminatorValue:
                    return TorchSkinJsonConverter.Read(json);
                case GreatswordSkinJsonConverter.DiscriminatorValue:
                    return GreatswordSkinJsonConverter.Read(json);
                case HarpoonGunSkinJsonConverter.DiscriminatorValue:
                    return HarpoonGunSkinJsonConverter.Read(json);
                case MaceSkinJsonConverter.DiscriminatorValue:
                    return MaceSkinJsonConverter.Read(json);
                case PistolSkinJsonConverter.DiscriminatorValue:
                    return PistolSkinJsonConverter.Read(json);
                case RifleSkinJsonConverter.DiscriminatorValue:
                    return RifleSkinJsonConverter.Read(json);
                case ShieldSkinJsonConverter.DiscriminatorValue:
                    return ShieldSkinJsonConverter.Read(json);
                case SpearSkinJsonConverter.DiscriminatorValue:
                    return SpearSkinJsonConverter.Read(json);
                case SwordSkinJsonConverter.DiscriminatorValue:
                    return SwordSkinJsonConverter.Read(json);
                case ToySkinJsonConverter.DiscriminatorValue:
                    return ToySkinJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new WeaponSkin
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Flags = SkinFlagsJsonConverter.Read(json.GetProperty("flags")),
            Races = json.GetProperty("races").GetList(static (in value) => value.GetEnum<RaceName>()),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString),
            DamageType = json.GetProperty("damage_type").GetEnum<DamageType>()
        };
    }

    public static void Write(Utf8JsonWriter writer, WeaponSkin value)
    {
        switch (value)
        {
            case AxeSkin axeSkin:
                AxeSkinJsonConverter.Write(writer, axeSkin);
                break;
            case SmallBundleSkin smallBundleSkin:
                SmallBundleSkinJsonConverter.Write(writer, smallBundleSkin);
                break;
            case HammerSkin hammerSkin:
                HammerSkinJsonConverter.Write(writer, hammerSkin);
                break;
            case StaffSkin staffSkin:
                StaffSkinJsonConverter.Write(writer, staffSkin);
                break;
            case LargeBundleSkin largeBundleSkin:
                LargeBundleSkinJsonConverter.Write(writer, largeBundleSkin);
                break;
            case TridentSkin tridentSkin:
                TridentSkinJsonConverter.Write(writer, tridentSkin);
                break;
            case WarhornSkin warhornSkin:
                WarhornSkinJsonConverter.Write(writer, warhornSkin);
                break;
            case ToyTwoHandedSkin toyTwoHandedSkin:
                ToyTwoHandedSkinJsonConverter.Write(writer, toyTwoHandedSkin);
                break;
            case ScepterSkin scepterSkin:
                ScepterSkinJsonConverter.Write(writer, scepterSkin);
                break;
            case DaggerSkin daggerSkin:
                DaggerSkinJsonConverter.Write(writer, daggerSkin);
                break;
            case FocusSkin focusSkin:
                FocusSkinJsonConverter.Write(writer, focusSkin);
                break;
            case ShortbowSkin shortbowSkin:
                ShortbowSkinJsonConverter.Write(writer, shortbowSkin);
                break;
            case LongbowSkin longbowSkin:
                LongbowSkinJsonConverter.Write(writer, longbowSkin);
                break;
            case TorchSkin torchSkin:
                TorchSkinJsonConverter.Write(writer, torchSkin);
                break;
            case GreatswordSkin greatswordSkin:
                GreatswordSkinJsonConverter.Write(writer, greatswordSkin);
                break;
            case HarpoonGunSkin harpoonGunSkin:
                HarpoonGunSkinJsonConverter.Write(writer, harpoonGunSkin);
                break;
            case MaceSkin maceSkin:
                MaceSkinJsonConverter.Write(writer, maceSkin);
                break;
            case PistolSkin pistolSkin:
                PistolSkinJsonConverter.Write(writer, pistolSkin);
                break;
            case RifleSkin rifleSkin:
                RifleSkinJsonConverter.Write(writer, rifleSkin);
                break;
            case ShieldSkin shieldSkin:
                ShieldSkinJsonConverter.Write(writer, shieldSkin);
                break;
            case SpearSkin spearSkin:
                SpearSkinJsonConverter.Write(writer, spearSkin);
                break;
            case SwordSkin swordSkin:
                SwordSkinJsonConverter.Write(writer, swordSkin);
                break;
            case ToySkin toySkin:
                ToySkinJsonConverter.Write(writer, toySkin);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(
                    EquipmentSkinJsonConverter.DiscriminatorName,
                    DiscriminatorValue
                );
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, WeaponSkin value)
    {
        EquipmentSkinJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteString("damage_type", value.DamageType.ToString());
    }
}
