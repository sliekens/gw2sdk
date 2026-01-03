using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a Jade Bot module, which adds new functionality to the Jade Bot.</summary>
[JsonConverter(typeof(JadeTechModuleJsonConverter))]
public sealed record JadeTechModule : Item;
