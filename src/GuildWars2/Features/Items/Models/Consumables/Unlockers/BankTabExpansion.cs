using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a bank tab expansion, which adds an extra bank tab to the account when consumed.</summary>
[JsonConverter(typeof(BankTabExpansionJsonConverter))]
public sealed record BankTabExpansion : Unlocker;
