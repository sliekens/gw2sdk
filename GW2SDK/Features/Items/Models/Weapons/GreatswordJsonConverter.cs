using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class GreatswordJsonConverter : JsonConverter<Greatsword>
{
    public const string DiscriminatorValue = "greatsword";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Greatsword).IsAssignableFrom(typeToConvert);
    }

    public override Greatsword Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Greatsword value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Greatsword Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(WeaponJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(WeaponJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(WeaponJsonConverter.DiscriminatorName).GetString()
            );
        }

        return new Greatsword
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes =
                json.GetProperty("game_types").GetList(static value => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetString(),
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
                    .GetMap(key => new Extensible<AttributeName>(key), value => value.GetInt32()),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            SecondarySuffixItemId = json.GetProperty("secondary_suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(value => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Greatsword value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            WeaponJsonConverter.DiscriminatorValue
        );
        writer.WriteString(WeaponJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        WeaponJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
