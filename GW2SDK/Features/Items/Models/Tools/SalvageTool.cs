using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record SalvageTool : Tool
{
    public required int Charges { get; init; }
}
