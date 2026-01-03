using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a teleport to friend consumable.</summary>
[JsonConverter(typeof(TeleportToFriendJsonConverter))]
public sealed record TeleportToFriend : Consumable;
