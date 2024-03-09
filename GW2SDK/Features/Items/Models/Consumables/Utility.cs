namespace GuildWars2.Items;

[PublicAPI]
public sealed record Utility : Consumable
{
    public required Effect? Effect { get; init; }
}
