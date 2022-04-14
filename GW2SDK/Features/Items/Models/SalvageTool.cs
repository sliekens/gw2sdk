using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record SalvageTool : Tool
{
    public int Charges { get; init; }
}