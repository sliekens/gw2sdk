namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxSong
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
