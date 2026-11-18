using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Skills;

/// <summary>An engineer's toolbelt skill.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record ToolbeltSkill : ActionSkill;
