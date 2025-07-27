using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class WeaponTypeJson
{
    public static Extensible<WeaponType> GetWeaponType(this in JsonElement json)
    {
        // The old name for harpoon gun is used in the API
        if (json.ValueEquals("Speargun"))
        {
            return WeaponType.HarpoonGun;
        }

        // Otherwise the enum value matches the string value
        return json.GetEnum<WeaponType>();
    }
}
