using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record SalvageTool : Tool
    {
        public int Charges { get; init; }
    }
}
