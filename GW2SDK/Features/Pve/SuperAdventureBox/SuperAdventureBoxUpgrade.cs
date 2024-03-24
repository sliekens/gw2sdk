namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Information about a super adventure box upgrade.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxUpgrade
{
    /// <summary>The upgrade ID.</summary>
    public required int Id { get; init; }

    /// <summary>The upgrade name.</summary>
    public required string Name { get; init; }
}
