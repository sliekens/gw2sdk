using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal sealed class SubskillJsonConverter : JsonConverter<Subskill>
{
    public override Subskill Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Subskill value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Subskill Read(in JsonElement json)
    {
        JsonElement idJson = json.GetProperty("id");
        JsonElement attunementJson = json.GetProperty("attunement");
        JsonElement formJson = json.GetProperty("form");
        return new Subskill
        {
            Id = idJson.GetInt32(),
            Attunement = attunementJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<GuildWars2.Hero.Attunement>?)attunementJson.GetEnum<GuildWars2.Hero.Attunement>(),
            Form = formJson.ValueKind == JsonValueKind.Null ? null : (GuildWars2.Extensible<Transformation>?)formJson.GetEnum<Transformation>()
        };
    }

    public static void Write(Utf8JsonWriter writer, Subskill value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        if (value.Attunement is { } presentAttunement)
        {
            writer.WriteString("attunement", presentAttunement.ToString());
        }
        else
        {
            writer.WriteNull("attunement");
        }
        if (value.Form is { } presentForm)
        {
            writer.WriteString("form", presentForm.ToString());
        }
        else
        {
            writer.WriteNull("form");
        }
        writer.WriteEndObject();
    }
}
