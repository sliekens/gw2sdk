using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class BankTabExpansionJsonConverter : JsonConverter<BankTabExpansion>
{
    public const string DiscriminatorValue = "bank_tab_expansion";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(BankTabExpansion).IsAssignableFrom(typeToConvert);
    }

    public override BankTabExpansion Read(
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
        BankTabExpansion value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static BankTabExpansion Read(JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetString();
        return new BankTabExpansion
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

    public static void Write(Utf8JsonWriter writer, BankTabExpansion value)
    {
        writer.WriteStartObject();
        writer.WriteString(
            ItemJsonConverter.DiscriminatorName,
            ConsumableJsonConverter.DiscriminatorValue
        );
        writer.WriteString(
            ConsumableJsonConverter.DiscriminatorName,
            UnlockerJsonConverter.DiscriminatorValue
        );
        writer.WriteString(UnlockerJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
