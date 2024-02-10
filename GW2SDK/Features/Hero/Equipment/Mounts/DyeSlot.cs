namespace GuildWars2.Hero.Equipment.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    public required int DyeId { get; init; }

    public required Material Material { get; init; }
}
