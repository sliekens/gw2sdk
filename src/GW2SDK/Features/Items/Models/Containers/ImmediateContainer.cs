using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a container that is opened immediately upon receipt.</summary>
[JsonConverter(typeof(ImmediateContainerJsonConverter))]
public sealed record ImmediateContainer : Container;
