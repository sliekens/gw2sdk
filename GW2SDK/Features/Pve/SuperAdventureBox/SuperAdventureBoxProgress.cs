namespace GuildWars2.Pve.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxProgress

{
    public required IReadOnlyCollection<SuperAdventureBoxZone> Zones { get; init; }

    public required IReadOnlyCollection<SuperAdventureBoxUpgrade> Unlocks { get; init; }

    public required IReadOnlyCollection<SuperAdventureBoxSong> Songs { get; init; }
}
