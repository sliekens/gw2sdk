using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training.Skills;

/// <summary>A short summary of an elite skill.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record EliteSkillSummary : SkillSummary;
