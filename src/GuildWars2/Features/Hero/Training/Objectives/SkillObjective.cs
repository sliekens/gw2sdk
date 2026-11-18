using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training.Objectives;

/// <summary>Informtion about a skill that can be unlocked by spending hero points.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record SkillObjective : TrainingObjective
{
    /// <summary>The skill ID.</summary>
    public required int SkillId { get; init; }
}
