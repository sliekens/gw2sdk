using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ArmorJsonConverter : JsonConverter<Armor>
{
    public const string DiscriminatorValue = "armor";

    public const string DiscriminatorName = "$armor_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Armor).IsAssignableFrom(typeToConvert);
    }

    public override Armor Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Armor value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Armor Read(in JsonElement json)
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
                case BootsJsonConverter.DiscriminatorValue:
                    return BootsJsonConverter.Read(json);
                case GlovesJsonConverter.DiscriminatorValue:
                    return GlovesJsonConverter.Read(json);
                case CoatJsonConverter.DiscriminatorValue:
                    return CoatJsonConverter.Read(json);
                case LeggingsJsonConverter.DiscriminatorValue:
                    return LeggingsJsonConverter.Read(json);
                case ShouldersJsonConverter.DiscriminatorValue:
                    return ShouldersJsonConverter.Read(json);
                case HelmJsonConverter.DiscriminatorValue:
                    return HelmJsonConverter.Read(json);
                case HelmAquaticJsonConverter.DiscriminatorValue:
                    return HelmAquaticJsonConverter.Read(json);
            }
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new Armor
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
            WeightClass = json.GetProperty("weight_class").GetEnum<WeightClass>(),
            Defense = json.GetProperty("defense").GetInt32(),
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(
                        static name => new Extensible<AttributeName>(name),
                        static (in JsonElement value) => value.GetInt32()
                    ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(static (in JsonElement value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Armor value)
    {
        switch (value)
        {
            case Boots boots:
                BootsJsonConverter.Write(writer, boots);
                break;
            case Gloves gloves:
                GlovesJsonConverter.Write(writer, gloves);
                break;
            case Coat coat:
                CoatJsonConverter.Write(writer, coat);
                break;
            case Leggings leggings:
                LeggingsJsonConverter.Write(writer, leggings);
                break;
            case Shoulders shoulders:
                ShouldersJsonConverter.Write(writer, shoulders);
                break;
            case Helm helm:
                HelmJsonConverter.Write(writer, helm);
                break;
            case HelmAquatic helmAquatic:
                HelmAquaticJsonConverter.Write(writer, helmAquatic);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Armor value)
    {
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("default_skin_id", value.DefaultSkinId);
        writer.WriteString("weight_class", value.WeightClass.ToString());
        writer.WriteNumber("defense", value.Defense);
        writer.WriteStartArray("infusion_slots");
        foreach (InfusionSlot slot in value.InfusionSlots)
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
        foreach (KeyValuePair<Extensible<AttributeName>, int> attribute in value.Attributes)
        {
            writer.WriteNumber(attribute.Key.ToString(), attribute.Value);
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

        writer.WriteStartArray("stat_choices");
        foreach (int statChoice in value.StatChoices)
        {
            writer.WriteNumberValue(statChoice);
        }

        writer.WriteEndArray();
    }
}
