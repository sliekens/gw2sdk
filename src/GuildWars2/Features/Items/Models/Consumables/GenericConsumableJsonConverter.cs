using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class GenericConsumableJsonConverter : JsonConverter<GenericConsumable>
{
    public const string DiscriminatorValue = "generic_consumable";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(GenericConsumable).IsAssignableFrom(typeToConvert);
    }

    public override GenericConsumable Read(
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
        GenericConsumable value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static GenericConsumable Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(ConsumableJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(ConsumableJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ConsumableJsonConverter.DiscriminatorName).GetString()
            );
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new GenericConsumable
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
            Effect = json.GetProperty("effect").GetNullable(EffectJsonConverter.Read),
            GuildUpgradeId = json.GetProperty("guild_upgrade_id").GetNullableInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GenericConsumable value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            ConsumableJsonConverter.DiscriminatorValue
        );
        writer.WriteString(ConsumableJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        if (value.Effect is not null)
        {
            writer.WritePropertyName("effect");
            EffectJsonConverter.Write(writer, value.Effect);
        }
        else
        {
            writer.WriteNull("effect");
        }

        if (value.GuildUpgradeId.HasValue)
        {
            writer.WriteNumber("guild_upgrade_id", value.GuildUpgradeId.Value);
        }
        else
        {
            writer.WriteNull("guild_upgrade_id");
        }

        writer.WriteEndObject();
    }
}
