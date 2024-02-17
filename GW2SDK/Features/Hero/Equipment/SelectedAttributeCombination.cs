namespace GuildWars2.Hero.Equipment;

/// <summary>Information about an attributes combination for equipment with selectable stats.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedAttributeCombination
{
    /// <summary>The ID of the combination which can be used to look up its name and base stats.</summary>
    public required int Id { get; init; }

    /// <summary>The effective attributes of the selected combination.</summary>
    public required IDictionary<AttributeName, int> Attributes { get; init; }
}
