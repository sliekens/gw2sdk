using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Recipes
{
    [PublicAPI]
    public enum Discipline
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Discipline)
        Armorsmith = 1,

        Artificer,

        Chef,

        Huntsman,

        Jeweler,

        Leatherworker,

        Scribe,

        Tailor,

        Weaponsmith
    }
}
