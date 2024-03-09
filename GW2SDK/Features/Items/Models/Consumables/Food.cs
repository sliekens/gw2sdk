namespace GuildWars2.Items;

[PublicAPI]
public sealed record Food : Consumable
{
    public required Effect? Effect { get; init; }
}
