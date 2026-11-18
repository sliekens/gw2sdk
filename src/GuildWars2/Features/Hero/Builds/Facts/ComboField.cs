using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A combo field that is placed by the skill/trait.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record ComboField : Fact
{
    /// <summary>The kind of field that is created.</summary>
    public required Extensible<ComboFieldName> Field { get; init; }
}
