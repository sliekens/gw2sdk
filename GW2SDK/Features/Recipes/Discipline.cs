using GW2SDK.Annotations;

namespace GW2SDK.Recipes
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
