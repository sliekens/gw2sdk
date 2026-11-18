using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

internal sealed class TraitedFactJsonConverter : JsonConverter<TraitedFact>
{
    public override TraitedFact Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TraitedFact value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TraitedFact Read(in JsonElement json)
    {
        JsonElement requiresTraitJson = json.GetProperty("requires_trait");
        JsonElement overridesJson = json.GetProperty("overrides");
        JsonElement factJson = json.GetProperty("fact");
        return new TraitedFact
        {
            RequiresTrait = requiresTraitJson.GetInt32(),
            Overrides = overridesJson.ValueKind == JsonValueKind.Null ? null : overridesJson.GetInt32(),
            Fact = FactJsonConverter.Read(factJson)
        };
    }

    public static void Write(Utf8JsonWriter writer, TraitedFact value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("requires_trait", value.RequiresTrait);
        if (value.Overrides is { } presentOverrides)
        {
            writer.WriteNumber("overrides", presentOverrides);
        }
        else
        {
            writer.WriteNull("overrides");
        }
        writer.WritePropertyName("fact");
        FactJsonConverter.Write(writer, value.Fact);
        writer.WriteEndObject();
    }
}
