using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillFlagsJson
{
    public static SkillFlags GetSkillFlags(this in JsonElement json)
    {
        var groundTargeted = false;
        var noUnderwater = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("GroundTargeted"))
            {
                groundTargeted = true;
            }
            else if (entry.ValueEquals("NoUnderwater"))
            {
                noUnderwater = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new SkillFlags
        {
            GroundTargeted = groundTargeted,
            NoUnderwater = noUnderwater,
            Other = others
        };
    }
}
