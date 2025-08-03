using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ItemRestrictionJsonConverter : JsonConverter<ItemRestriction>
{
    public override ItemRestriction Read(
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
        ItemRestriction value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ItemRestriction Read(in JsonElement value)
    {
        return new()
        {
            Races =
                value.GetProperty("races").GetList(static (in JsonElement value) => value.GetEnum<RaceName>()),
            Professions =
                value.GetProperty("professions")
                    .GetList(static (in JsonElement value) => value.GetEnum<ProfessionName>()),
            BodyTypes =
                value.GetProperty("body_types")
                    .GetList(static (in JsonElement value) => value.GetEnum<BodyType>()),
            Other = value.GetProperty("other")
                .GetList(static (in JsonElement value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, ItemRestriction value)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("races");
        foreach (Extensible<RaceName> race in value.Races)
        {
            writer.WriteStringValue(race.ToString());
        }

        writer.WriteEndArray();
        writer.WriteStartArray("professions");
        foreach (Extensible<ProfessionName> profession in value.Professions)
        {
            writer.WriteStringValue(profession.ToString());
        }

        writer.WriteEndArray();

        writer.WriteStartArray("body_types");
        foreach (Extensible<BodyType> bodyType in value.BodyTypes)
        {
            writer.WriteStringValue(bodyType.ToString());
        }

        writer.WriteEndArray();

        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
