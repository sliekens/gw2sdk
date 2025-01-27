using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>Information about how many legendary items are stored in the legendary armory of the current account.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(BoundLegendaryItemJsonConverter))]
public sealed record BoundLegendaryItem
{
    /// <summary>The item id.</summary>
    public required int Id { get; init; }

    /// <summary>How many of the current legendary item are bound to the account.</summary>
    public required int Count { get; init; }
}
