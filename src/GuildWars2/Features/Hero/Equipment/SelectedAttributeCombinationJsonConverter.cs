using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal sealed class
    SelectedAttributeCombinationJsonConverter : JsonConverter<SelectedAttributeCombination>
{
    public override SelectedAttributeCombination Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static SelectedAttributeCombination Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Attributes = json.GetProperty("attributes")
                .GetMap(
                    static name => new Extensible<AttributeName>(name),
                    static (in value) => value.GetInt32()
                )
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        SelectedAttributeCombination value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, SelectedAttributeCombination value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteStartObject("attributes");
        foreach (KeyValuePair<Extensible<AttributeName>, int> pair in value.Attributes)
        {
            writer.WriteNumber(pair.Key.ToString(), pair.Value);
        }

        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}
