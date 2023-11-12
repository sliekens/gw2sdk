namespace GuildWars2.Pve.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxUpgrade
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
