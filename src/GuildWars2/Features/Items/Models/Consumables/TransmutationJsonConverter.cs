using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class TransmutationJsonConverter : JsonConverter<Transmutation>
{
    public const string DiscriminatorValue = "transmutation";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Transmutation).IsAssignableFrom(typeToConvert);
    }

    public override Transmutation Read(
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
        Transmutation value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Transmutation Read(in JsonElement json)
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
        return new Transmutation
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
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            SkinIds = json.GetProperty("skin_ids").GetList(static (in value) => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, Transmutation value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            ConsumableJsonConverter.DiscriminatorValue
        );
        writer.WriteString(ConsumableJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteStartArray("skin_ids");
        foreach (int id in value.SkinIds)
        {
            writer.WriteNumberValue(id);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
