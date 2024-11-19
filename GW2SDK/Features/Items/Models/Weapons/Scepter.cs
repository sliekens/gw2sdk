using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a scepter.</summary>
[PublicAPI]
[JsonConverter(typeof(ScepterJsonConverter))]
public sealed record Scepter : Weapon;
