namespace GuildWars2.Crafting;

[PublicAPI]
[DataTransferObject]
public sealed record GuildIngredient
{
    public required int UpgradeId { get; init; }

    public required int Count { get; init; }
}
