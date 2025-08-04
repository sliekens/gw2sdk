namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Information about a Super Adventure Box song.</summary>
[DataTransferObject]
public sealed record SuperAdventureBoxSong
{
    /// <summary>The song ID.</summary>
    public required int Id { get; init; }

    /// <summary>The song name.</summary>
    public required string Name { get; init; }
}
