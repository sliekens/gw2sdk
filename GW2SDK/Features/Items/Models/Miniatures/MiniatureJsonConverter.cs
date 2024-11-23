
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class MiniatureJsonConverter : JsonConverter<Miniature>
{
    public const string DiscriminatorValue = "miniature";

    public override Miniature? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Miniature value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Miniature Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString());
        }

        return new Miniature
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
            MiniatureId = json.GetProperty("miniature_id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Miniature value)
    {
        writer.WriteStartObject();
        writer.WriteString(ItemJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("miniature_id", value.MiniatureId);
        writer.WriteEndObject();
    }
}
