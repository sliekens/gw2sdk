using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a teleport to friend consumable.</summary>
[PublicAPI]
[JsonConverter(typeof(TeleportToFriendJsonConverter))]
public sealed record TeleportToFriend : Consumable;
