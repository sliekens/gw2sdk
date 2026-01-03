using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class BlackLionChestJsonConverter : JsonConverter<BlackLionChest>
{
    public const string DiscriminatorValue = "black_lion_chest";

    public override BlackLionChest? Read(
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
        BlackLionChest value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static BlackLionChest Read(in JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(ContainerJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(ContainerJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ContainerJsonConverter.DiscriminatorName).GetString()
            );
        }

        string? iconString = json.GetProperty("icon").GetString();
        return new BlackLionChest
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
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }

    public static void Write(Utf8JsonWriter writer, BlackLionChest value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            ContainerJsonConverter.DiscriminatorValue
        );
        writer.WriteString(ContainerJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
