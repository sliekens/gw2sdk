using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class WeaponJsonConverter : JsonConverter<Weapon>
{
    public const string DiscriminatorValue = "weapon";

    public const string DiscriminatorName = "$weapon_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Weapon).IsAssignableFrom(typeToConvert);
    }

    public override Weapon Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Weapon value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Weapon Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case AxeJsonConverter.DiscriminatorValue:
                    return AxeJsonConverter.Read(json);
                case SmallBundleJsonConverter.DiscriminatorValue:
                    return SmallBundleJsonConverter.Read(json);
                case HammerJsonConverter.DiscriminatorValue:
                    return HammerJsonConverter.Read(json);
                case StaffJsonConverter.DiscriminatorValue:
                    return StaffJsonConverter.Read(json);
                case LargeBundleJsonConverter.DiscriminatorValue:
                    return LargeBundleJsonConverter.Read(json);
                case TridentJsonConverter.DiscriminatorValue:
                    return TridentJsonConverter.Read(json);
                case WarhornJsonConverter.DiscriminatorValue:
                    return WarhornJsonConverter.Read(json);
                case ToyTwoHandedJsonConverter.DiscriminatorValue:
                    return ToyTwoHandedJsonConverter.Read(json);
                case ScepterJsonConverter.DiscriminatorValue:
                    return ScepterJsonConverter.Read(json);
                case DaggerJsonConverter.DiscriminatorValue:
                    return DaggerJsonConverter.Read(json);
                case FocusJsonConverter.DiscriminatorValue:
                    return FocusJsonConverter.Read(json);
                case ShortbowJsonConverter.DiscriminatorValue:
                    return ShortbowJsonConverter.Read(json);
                case LongbowJsonConverter.DiscriminatorValue:
                    return LongbowJsonConverter.Read(json);
                case TorchJsonConverter.DiscriminatorValue:
                    return TorchJsonConverter.Read(json);
                case GreatswordJsonConverter.DiscriminatorValue:
                    return GreatswordJsonConverter.Read(json);
                case HarpoonGunJsonConverter.DiscriminatorValue:
                    return HarpoonGunJsonConverter.Read(json);
                case MaceJsonConverter.DiscriminatorValue:
                    return MaceJsonConverter.Read(json);
                case PistolJsonConverter.DiscriminatorValue:
                    return PistolJsonConverter.Read(json);
                case RifleJsonConverter.DiscriminatorValue:
                    return RifleJsonConverter.Read(json);
                case ShieldJsonConverter.DiscriminatorValue:
                    return ShieldJsonConverter.Read(json);
                case SpearJsonConverter.DiscriminatorValue:
                    return SpearJsonConverter.Read(json);
                case SwordJsonConverter.DiscriminatorValue:
                    return SwordJsonConverter.Read(json);
                case ToyJsonConverter.DiscriminatorValue:
                    return ToyJsonConverter.Read(json);
            }
        }

        var iconString = json.GetProperty("icon").GetString();
        return new Weapon
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static (in JsonElement value) => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            DefaultSkinId = json.GetProperty("default_skin_id").GetInt32(),
            DamageType = json.GetProperty("damage_type").GetEnum<DamageType>(),
            MinPower = json.GetProperty("min_power").GetInt32(),
            MaxPower = json.GetProperty("max_power").GetInt32(),
            Defense = json.GetProperty("defense").GetInt32(),
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(static key => new Extensible<AttributeName>(key), static (in JsonElement value) => value.GetInt32()),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            SecondarySuffixItemId = json.GetProperty("secondary_suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(static (in JsonElement value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Weapon value)
    {
        switch (value)
        {
            case Axe axe:
                AxeJsonConverter.Write(writer, axe);
                break;
            case SmallBundle smallBundle:
                SmallBundleJsonConverter.Write(writer, smallBundle);
                break;
            case Hammer hammer:
                HammerJsonConverter.Write(writer, hammer);
                break;
            case Staff staff:
                StaffJsonConverter.Write(writer, staff);
                break;
            case LargeBundle largeBundle:
                LargeBundleJsonConverter.Write(writer, largeBundle);
                break;
            case Trident trident:
                TridentJsonConverter.Write(writer, trident);
                break;
            case Warhorn warhorn:
                WarhornJsonConverter.Write(writer, warhorn);
                break;
            case ToyTwoHanded toyTwoHanded:
                ToyTwoHandedJsonConverter.Write(writer, toyTwoHanded);
                break;
            case Scepter scepter:
                ScepterJsonConverter.Write(writer, scepter);
                break;
            case Dagger dagger:
                DaggerJsonConverter.Write(writer, dagger);
                break;
            case Focus focus:
                FocusJsonConverter.Write(writer, focus);
                break;
            case Shortbow shortbow:
                ShortbowJsonConverter.Write(writer, shortbow);
                break;
            case Longbow longbow:
                LongbowJsonConverter.Write(writer, longbow);
                break;
            case Torch torch:
                TorchJsonConverter.Write(writer, torch);
                break;
            case Greatsword greatsword:
                GreatswordJsonConverter.Write(writer, greatsword);
                break;
            case HarpoonGun harpoonGun:
                HarpoonGunJsonConverter.Write(writer, harpoonGun);
                break;
            case Mace mace:
                MaceJsonConverter.Write(writer, mace);
                break;
            case Pistol pistol:
                PistolJsonConverter.Write(writer, pistol);
                break;
            case Rifle rifle:
                RifleJsonConverter.Write(writer, rifle);
                break;
            case Shield shield:
                ShieldJsonConverter.Write(writer, shield);
                break;
            case Spear spear:
                SpearJsonConverter.Write(writer, spear);
                break;
            case Sword sword:
                SwordJsonConverter.Write(writer, sword);
                break;
            case Toy toy:
                ToyJsonConverter.Write(writer, toy);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                ItemJsonConverter.WriteCommonProperties(writer, value);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Weapon value)
    {
        writer.WriteNumber("default_skin_id", value.DefaultSkinId);
        writer.WriteString("damage_type", value.DamageType.ToString());
        writer.WriteNumber("min_power", value.MinPower);
        writer.WriteNumber("max_power", value.MaxPower);
        writer.WriteNumber("defense", value.Defense);
        writer.WriteStartArray("infusion_slots");
        foreach (var slot in value.InfusionSlots)
        {
            InfusionSlotJsonConverter.Write(writer, slot);
        }

        writer.WriteEndArray();
        writer.WriteNumber("attribute_adjustment", value.AttributeAdjustment);
        if (value.AttributeCombinationId.HasValue)
        {
            writer.WriteNumber("attribute_combination_id", value.AttributeCombinationId.Value);
        }
        else
        {
            writer.WriteNull("attribute_combination_id");
        }

        writer.WriteStartObject("attributes");
        foreach (var attribute in value.Attributes)
        {
            writer.WritePropertyName(attribute.Key.ToString());
            writer.WriteNumberValue(attribute.Value);
        }

        writer.WriteEndObject();
        if (value.Buff is not null)
        {
            writer.WritePropertyName("buff");
            BuffJsonConverter.Write(writer, value.Buff);
        }
        else
        {
            writer.WriteNull("buff");
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

        writer.WriteStartArray("stat_choices");
        foreach (var stat in value.StatChoices)
        {
            writer.WriteNumberValue(stat);
        }

        writer.WriteEndArray();
    }
}
