using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")] // Names are copied from API data
    public enum SkillSlot
    {
        Weapon_1 = 1,

        Weapon_2,

        Weapon_3,

        Weapon_4,

        Weapon_5,

        Heal,

        Utility,

        Elite,

        Profession_1,

        Profession_2,

        Profession_3,

        Profession_4,

        Profession_5,

        Downed_1,

        Downed_2,

        Downed_3,

        Downed_4
    }
}
