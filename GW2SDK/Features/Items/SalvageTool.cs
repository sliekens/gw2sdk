using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public sealed class SalvageTool : Tool
    {
        public int Charges { get; set; }
    }
}