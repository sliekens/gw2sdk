using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record SalvageTool : Tool
{
    public required int Charges { get; init; }
}
