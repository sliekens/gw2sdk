namespace GuildWars2.ItemStats;

[PublicAPI]
[DataTransferObject]
public sealed record ItemStatAttribute
{
    public required UpgradeAttributeName Attribute { get; init; }

    public required double Multiplier { get; init; }

    public required int Value { get; init; }
}
