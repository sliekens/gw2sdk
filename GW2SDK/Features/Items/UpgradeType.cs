namespace GW2SDK.Features.Items
{
    public enum UpgradeType
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(UpgradeType)
        Attunement = 1,

        Infusion
    }
}