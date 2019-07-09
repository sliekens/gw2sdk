using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum CraftingDiscipline
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(CraftingDiscipline)
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
