namespace GuildWars2.Hero.Wardrobe;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    public required int ColorId { get; init; }

    public required Material Material { get; init; }
}
