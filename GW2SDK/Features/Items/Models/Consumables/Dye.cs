namespace GuildWars2.Items;

[PublicAPI]
public sealed record Dye : Unlocker
{
    public required int ColorId { get; init; }
}
