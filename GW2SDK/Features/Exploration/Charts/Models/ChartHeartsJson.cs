using System;
using System.Collections.Generic;
using System.Text.Json;
using GuildWars2.Exploration.Hearts;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.Charts;

[PublicAPI]
public static class ChartHeartsJson
{
    public static Dictionary<int, Heart> GetChartHearts(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, Heart> tasks = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                tasks[id] = member.Value.GetHeart(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return tasks;
    }
}
