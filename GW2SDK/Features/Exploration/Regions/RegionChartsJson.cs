using System.Text.Json;
using GuildWars2.Exploration.Charts;

namespace GuildWars2.Exploration.Regions;

[PublicAPI]
public static class RegionChartsJson
{
    public static Dictionary<int, Chart> GetRegionCharts(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, Chart> maps = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                maps[id] = member.Value.GetChart(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return maps;
    }
}
