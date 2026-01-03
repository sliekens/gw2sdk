using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment;

/// <summary>Information about an attributes combination for equipment with selectable stats.</summary>
[DataTransferObject]
[JsonConverter(typeof(SelectedAttributeCombinationJsonConverter))]
public sealed record SelectedAttributeCombination
{
    /// <summary>The ID of the combination which can be used to look up its name and base stats.</summary>
    public required int Id { get; init; }

    // TODO: should have been Extensible<AttributeName> instead of AttributeName
    /// <summary>The effective attributes of the selected combination.</summary>
    public required IDictionary<AttributeName, int> Attributes { get; init; }
}
