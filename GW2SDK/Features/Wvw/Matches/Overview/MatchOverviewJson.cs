using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Overview;

internal static class MatchOverviewJson
{
    public static MatchOverview GetMatchOverview(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember worlds = "worlds";
        RequiredMember allWorlds = "all_worlds";
        RequiredMember startTime = "start_time";
        RequiredMember endTime = "end_time";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (worlds.Match(member))
            {
                worlds = member;
            }
            else if (allWorlds.Match(member))
            {
                allWorlds = member;
            }
            else if (startTime.Match(member))
            {
                startTime = member;
            }
            else if (endTime.Match(member))
            {
                endTime = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MatchOverview
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Worlds = worlds.Map(static (in JsonElement value) => value.GetWorlds()),
            AllWorlds = allWorlds.Map(static (in JsonElement value) => value.GetAllWorlds()),
            StartTime = startTime.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            EndTime = endTime.Map(static (in JsonElement value) => value.GetDateTimeOffset())
        };
    }
}
