using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class BaitJsonConverter : JsonConverter<Bait>
{
    public const string DiscriminatorValue = "bait";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Bait).IsAssignableFrom(typeToConvert);
    }

    public override Bait Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Bait value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Bait Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName)
            .ValueEquals(GatheringToolJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString()
            );
        }

        if (!json.GetProperty(GatheringToolJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(GatheringToolJsonConverter.DiscriminatorName).GetString()
            );
        }

        return new Bait
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
            IconHref = json.GetProperty("icon").GetString()
        };
    }

    public static void Write(Utf8JsonWriter writer, Bait value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            GatheringToolJsonConverter.DiscriminatorValue
        );
        writer.WriteString(GatheringToolJsonConverter.DiscriminatorName, DiscriminatorValue);
        GatheringToolJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
