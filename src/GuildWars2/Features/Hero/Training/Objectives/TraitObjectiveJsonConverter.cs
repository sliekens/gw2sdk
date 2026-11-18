using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training.Objectives;

internal sealed class TraitObjectiveJsonConverter : JsonConverter<TraitObjective>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "trait_objective";

    public override TraitObjective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TraitObjective value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TraitObjective Read(in JsonElement json)
    {
        if (!json.GetProperty(DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(DiscriminatorName).GetString());
        }

        JsonElement costJson = json.GetProperty("cost");
        JsonElement traitIdJson = json.GetProperty("trait_id");
        return new TraitObjective
        {
            Cost = costJson.GetInt32(),
            TraitId = traitIdJson.GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, TraitObjective value)
    {
        writer.WriteStartObject();
        writer.WriteString(DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("cost", value.Cost);
        writer.WriteNumber("trait_id", value.TraitId);
        writer.WriteEndObject();
    }
}
