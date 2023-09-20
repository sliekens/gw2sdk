namespace GuildWars2.ItemStats;

/// <summary>A reference to a named set of item attributes, used for customizable items such as legendary weapons. The Id
/// can be used to retrieve more information.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedStat
{
    public required int Id { get; init; }

    public required SelectedModification Attributes { get; init; }
}
