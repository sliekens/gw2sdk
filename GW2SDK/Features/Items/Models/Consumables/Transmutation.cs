namespace GuildWars2.Items;

[PublicAPI]
public sealed record Transmutation : Consumable
{
    public required IReadOnlyCollection<int> SkinIds { get; init; }
}
