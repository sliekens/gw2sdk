using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a dagger.</summary>
[JsonConverter(typeof(DaggerJsonConverter))]
public sealed record Dagger : Weapon;
