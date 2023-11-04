using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds;

internal static class SkillFlagsJson
{
    public static SkillFlags GetSkillFlags(
        this JsonElement json
    )
    {
        var groundTargeted = false;
        var noUnderwater = false;
        List<string>? others = null;
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
                others ??= new List<string>();
                others.Add(entry.GetStringRequired());
            }
        }

        return new SkillFlags
        {
            GroundTargeted = groundTargeted,
            NoUnderwater = noUnderwater,
            Other = others ?? (IReadOnlyCollection<string>)Array.Empty<string>()
        };
    }
}
