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
                    static name => name switch
                    {
                        nameof(AttributeName.None) => AttributeName.None,
                        nameof(AttributeName.Power) => AttributeName.Power,
                        nameof(AttributeName.Precision) => AttributeName.Precision,
                        nameof(AttributeName.Toughness) => AttributeName.Toughness,
                        nameof(AttributeName.Vitality) => AttributeName.Vitality,
                        nameof(AttributeName.Concentration) => AttributeName.Concentration,
                        nameof(AttributeName.ConditionDamage) => AttributeName.ConditionDamage,
                        nameof(AttributeName.Expertise) => AttributeName.Expertise,
                        nameof(AttributeName.Ferocity) => AttributeName.Ferocity,
                        nameof(AttributeName.HealingPower) => AttributeName.HealingPower,
                        nameof(AttributeName.AgonyResistance) => AttributeName.AgonyResistance,
                        _ => throw new JsonException()
                    },
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
        foreach (KeyValuePair<AttributeName, int> pair in value.Attributes)
        {
            writer.WriteNumber(pair.Key.ToString(), pair.Value);
        }

        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}
