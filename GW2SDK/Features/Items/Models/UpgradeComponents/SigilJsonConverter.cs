
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class SigilJsonConverter : JsonConverter<Sigil>
{
    public const string DiscriminatorValue = "sigil";

    public override Sigil? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Sigil value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Sigil Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(UpgradeComponentJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(UpgradeComponentJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(UpgradeComponentJsonConverter.DiscriminatorName).GetString()
            );
        }

        return new Sigil
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
            IconHref = json.GetProperty("icon").GetString(),
            UpgradeComponentFlags = UpgradeComponentFlagsJsonConverter.Read(json.GetProperty("upgrade_component_flags")),
            InfusionUpgradeFlags = InfusionSlotFlagsJsonConverter.Read(json.GetProperty("infusion_upgrade_flags")),
            AttributeAdjustment = json.GetProperty("attribute_adjustment").GetDouble(),
            AttributeCombinationId = json.GetProperty("attribute_combination_id").GetNullableInt32(),
            Attributes = json.GetProperty("attributes").GetMap(
                static name => new Extensible<AttributeName>(name),
                static value => value.GetInt32()
            ),
            Buff = json.GetProperty("buff").GetNullable(BuffJsonConverter.Read),
            SuffixName = json.GetProperty("suffix").GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, Sigil value)
    {
        writer.WriteStartObject();
        writer.WriteString(ItemJsonConverter.DiscriminatorName, UpgradeComponentJsonConverter.DiscriminatorValue);
        writer.WriteString(UpgradeComponentJsonConverter.DiscriminatorName, DiscriminatorValue);
        UpgradeComponentJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
