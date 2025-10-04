using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class AmuletJsonConverter : JsonConverter<Amulet>
{
    public const string DiscriminatorValue = "amulet";

    public override Amulet Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Amulet value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Amulet Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(TrinketJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(TrinketJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(TrinketJsonConverter.DiscriminatorName).GetString()
            );
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new Amulet
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
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            InfusionSlots = json.GetProperty("infusion_slots")
                .GetList(InfusionSlotJsonConverter.Read),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId =
                json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes =
                json.GetProperty("attributes")
                    .GetMap(
                        static name => new Extensible<AttributeName>(name),
                        static (in value) => value.GetInt32()
                    ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixItemId = json.GetProperty("suffix_item_id").GetNullableInt32(),
            StatChoices = json.GetProperty("stat_choices").GetList(static (in value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Amulet value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            TrinketJsonConverter.DiscriminatorValue
        );
        writer.WriteString(TrinketJsonConverter.DiscriminatorName, DiscriminatorValue);
        TrinketJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
