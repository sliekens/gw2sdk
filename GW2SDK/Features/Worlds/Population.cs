namespace GW2SDK.Features.Worlds
{
    public enum Population
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Population)
        Low = 1,

        Medium,

        High,

        VeryHigh,

        Full
    }
}
