namespace GuildWars2.Hero.Equipment;

/// <summary>Information about the stats of equipment with selectable stats.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedStat
{
    /// <summary>The ID of the selected stat which can be used to look up details of the stat.</summary>
    public required int Id { get; init; }

    /// <summary>The attributes of the selected stat.</summary>
    public required SelectedModification Attributes { get; init; }
}
