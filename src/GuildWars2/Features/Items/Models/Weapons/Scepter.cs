using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a scepter.</summary>
[JsonConverter(typeof(ScepterJsonConverter))]
public sealed record Scepter : Weapon;
