using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public enum InfusionSlotFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(InfusionSlotFlag)
        Enrichment = 1,

        Infusion
    }
}