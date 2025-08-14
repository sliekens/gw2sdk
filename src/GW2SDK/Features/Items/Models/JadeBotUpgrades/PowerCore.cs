using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a Jade Bot power core, which is needed to receive benefits from your Jade Bot. Equipping a
/// power core gives stat bonuses and allows the Jade Bot to perform actions and to make use of Jade Tech modules.</summary>
[JsonConverter(typeof(PowerCoreJsonConverter))]
public sealed record PowerCore : Item;
