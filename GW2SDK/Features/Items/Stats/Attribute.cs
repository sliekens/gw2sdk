using GuildWars2.Hero;

namespace GuildWars2.Items.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record Attribute
{
    public required AttributeName Name { get; init; }

    public required double Multiplier { get; init; }

    public required int Value { get; init; }
}
