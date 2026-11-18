using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Skills;

/// <summary>A Ranger's pet skill.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record PetSkill : ActionSkill;
