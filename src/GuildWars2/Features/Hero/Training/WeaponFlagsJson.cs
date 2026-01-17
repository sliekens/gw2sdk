using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponFlagsJson
{
    public static WeaponFlags GetWeaponFlags(this in JsonElement json)
    {
        bool mainhand = false;
        bool offhand = false;
        bool twoHand = false;
        bool aquatic = false;
        ImmutableList<string>.Builder others = ImmutableList.CreateBuilder<string>();
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Mainhand"))
            {
                mainhand = true;
            }
            else if (entry.ValueEquals("Offhand"))
            {
                offhand = true;
            }
            else if (entry.ValueEquals("TwoHand"))
            {
                twoHand = true;
            }
            else if (entry.ValueEquals("Aquatic"))
            {
                aquatic = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new WeaponFlags
        {
            Mainhand = mainhand,
            Offhand = offhand,
            TwoHand = twoHand,
            Aquatic = aquatic,
            Other = new ImmutableValueList<string>(others.ToImmutable())
        };
    }
}
