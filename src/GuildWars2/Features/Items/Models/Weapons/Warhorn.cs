using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a warhorn.</summary>
[JsonConverter(typeof(WarhornJsonConverter))]
public sealed record Warhorn : Weapon;
