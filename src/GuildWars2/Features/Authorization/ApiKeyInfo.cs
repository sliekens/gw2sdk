using System.Text.Json.Serialization;

namespace GuildWars2.Authorization;

/// <summary>Information about an API key created from the account management page.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record ApiKeyInfo : TokenInfo;
