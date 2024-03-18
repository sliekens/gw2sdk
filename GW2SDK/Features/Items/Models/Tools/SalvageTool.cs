namespace GuildWars2.Items;

[PublicAPI]
public sealed record SalvageTool : Item
{
    public required int Charges { get; init; }
}
