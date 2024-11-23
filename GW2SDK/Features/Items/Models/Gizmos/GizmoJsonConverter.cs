using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class GizmoJsonConverter : JsonConverter<Gizmo>
{
    public const string DiscriminatorValue = "gizmo";

    public const string DiscriminatorName = "$gizmo_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Gizmo).IsAssignableFrom(typeToConvert);
    }

    public override Gizmo Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Gizmo value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Gizmo Read(JsonElement json)
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
                case BlackLionChestKeyJsonConverter.DiscriminatorValue:
                    return BlackLionChestKeyJsonConverter.Read(json);
                case RentableContractNpcJsonConverter.DiscriminatorValue:
                    return RentableContractNpcJsonConverter.Read(json);
                case UnlimitedConsumableJsonConverter.DiscriminatorValue:
                    return UnlimitedConsumableJsonConverter.Read(json);
            }
        }

        return new Gizmo
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
            GuildUpgradeId = json.GetProperty("guild_upgrade_id").GetNullableInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Gizmo value)
    {
        switch (value)
        {
            case BlackLionChestKey blackLionChestKey:
                BlackLionChestKeyJsonConverter.Write(writer, blackLionChestKey);
                break;
            case RentableContractNpc rentableContractNpc:
                RentableContractNpcJsonConverter.Write(writer, rentableContractNpc);
                break;
            case UnlimitedConsumable unlimitedConsumable:
                UnlimitedConsumableJsonConverter.Write(writer, unlimitedConsumable);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Gizmo value)
    {
        ItemJsonConverter.WriteCommonProperties(writer, value);
        if (value.GuildUpgradeId.HasValue)
        {
            writer.WriteNumber("guild_upgrade_id", value.GuildUpgradeId.Value);
        }
        else
        {
            writer.WriteNull("guild_upgrade_id");
        }
    }
}
