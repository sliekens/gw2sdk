using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ContainerJsonConverter : JsonConverter<Container>
{
    public const string DiscriminatorValue = "container";

    public const string DiscriminatorName = "$container_type";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Container).IsAssignableFrom(typeToConvert);
    }

    public override Container Read(
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
        Container value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Container Read(JsonElement json)
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
                case BlackLionChestJsonConverter.DiscriminatorValue:
                    return BlackLionChestJsonConverter.Read(json);
                case GiftBoxJsonConverter.DiscriminatorValue:
                    return GiftBoxJsonConverter.Read(json);
                case ImmediateContainerJsonConverter.DiscriminatorValue:
                    return ImmediateContainerJsonConverter.Read(json);
            }
        }

        var iconString = json.GetProperty("icon").GetString();
        return new Container
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
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }

    public static void Write(Utf8JsonWriter writer, Container value)
    {
        switch (value)
        {
            case BlackLionChest blackLionChest:
                BlackLionChestJsonConverter.Write(writer, blackLionChest);
                break;
            case GiftBox giftBox:
                GiftBoxJsonConverter.Write(writer, giftBox);
                break;
            case ImmediateContainer immediateContainer:
                ImmediateContainerJsonConverter.Write(writer, immediateContainer);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                ItemJsonConverter.WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }
}
