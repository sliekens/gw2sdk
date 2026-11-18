using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training;

internal sealed class TrainingObjectiveJsonConverter : JsonConverter<TrainingObjective>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "training_objective";

    public override TrainingObjective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TrainingObjective value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TrainingObjective Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case Objectives.SkillObjectiveJsonConverter.DiscriminatorValue:
                    return Objectives.SkillObjectiveJsonConverter.Read(json);
                case Objectives.TraitObjectiveJsonConverter.DiscriminatorValue:
                    return Objectives.TraitObjectiveJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement costJson = json.GetProperty("cost");
        return new TrainingObjective
        {
            Cost = costJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, TrainingObjective value)
    {
        if (value is Objectives.SkillObjective skillObjective)
        {
            Objectives.SkillObjectiveJsonConverter.Write(writer, skillObjective);
        }
        else if (value is Objectives.TraitObjective traitObjective)
        {
            Objectives.TraitObjectiveJsonConverter.Write(writer, traitObjective);
        }
        else if (value.GetType() == typeof(TrainingObjective))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteNumber("cost", value.Cost);
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
