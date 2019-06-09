namespace GW2SDK.Features.Items
{
    public enum DamageType
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(DamageType)
        Choking = 1,

        Fire,

        Ice,

        Lightning,

        Physical
    }
}