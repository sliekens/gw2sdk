using GuildWars2.Hero;

namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record UpgradeAttribute
{
    public required CombatAttribute Attribute { get; init; }

    public required int Modifier { get; init; }
}
