
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal sealed class SelectedAttributeCombinationJsonConverter : JsonConverter<SelectedAttributeCombination>
{
    public override SelectedAttributeCombination? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SelectedAttributeCombination value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SelectedAttributeCombination? Read(JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return new SelectedAttributeCombination
        {
            Id = json.GetProperty("id").GetInt32(),
            Attributes = json.GetProperty("attributes").GetMap(
                static value => value switch
                {
                    nameof(AttributeName.Power) => AttributeName.Power,
                    nameof(AttributeName.Precision) => AttributeName.Precision,
                    nameof(AttributeName.Toughness) => AttributeName.Toughness,
                    nameof(AttributeName.Vitality) => AttributeName.Vitality,
                    nameof(AttributeName.Concentration) => AttributeName.Concentration,
                    nameof(AttributeName.ConditionDamage) => AttributeName.ConditionDamage,
                    nameof(AttributeName.Ferocity) => AttributeName.Ferocity,
                    nameof(AttributeName.Expertise) => AttributeName.Expertise,
                    nameof(AttributeName.HealingPower) => AttributeName.HealingPower,
                    nameof(AttributeName.AgonyResistance) => AttributeName.AgonyResistance,
                    _ => throw new InvalidOperationException($"Unexpected attribute name: {value}")
                },
                static value => value.GetInt32()
            )
        };
    }

    public static void Write(Utf8JsonWriter writer, SelectedAttributeCombination value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WritePropertyName("attributes");
        writer.WriteStartObject();
        foreach (var attribute in value.Attributes)
        {
            writer.WriteNumber(attribute.Key.ToString(), attribute.Value);
        }

        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}
