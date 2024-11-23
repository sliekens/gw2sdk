using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class ItemRestrictionJsonConverter : JsonConverter<ItemRestriction>
{
    public override ItemRestriction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ItemRestriction value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ItemRestriction Read(JsonElement value)
    {
        return new ItemRestriction
        {
            Races = value.GetProperty("races").GetSet(static value => value.GetEnum<RaceName>()),
            Professions = value.GetProperty("professions").GetSet(static value => value.GetEnum<ProfessionName>()),
            BodyTypes = value.GetProperty("body_types").GetSet(static value => value.GetEnum<BodyType>()),
            Other = value.GetProperty("other").GetList(static value => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, ItemRestriction value)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("races");
        writer.WriteStartArray();
        foreach (var race in value.Races)
        {
            writer.WriteStringValue(race.ToString());
        }

        writer.WriteEndArray();
        writer.WritePropertyName("professions");
        writer.WriteStartArray();
        foreach (var profession in value.Professions)
        {
            writer.WriteStringValue(profession.ToString());
        }

        writer.WriteEndArray();

        writer.WritePropertyName("body_types");
        writer.WriteStartArray();
        foreach (var bodyType in value.BodyTypes)
        {
            writer.WriteStringValue(bodyType.ToString());
        }

        writer.WriteEndArray();

        writer.WritePropertyName("other");
        writer.WriteStartArray();
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();

        writer.WriteEndObject();
    }
}
