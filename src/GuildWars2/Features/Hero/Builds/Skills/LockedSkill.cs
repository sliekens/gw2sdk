using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Skills;

/// <summary>Placeholder 'Locked' skill. It is only used to lock other skills while transformed.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record LockedSkill : ActionSkill;
