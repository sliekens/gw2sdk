using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Overview;

internal static class MatchOverviewJson
{
    public static MatchOverview GetMatchOverview(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember worlds = "worlds";
        RequiredMember allWorlds = "all_worlds";
        RequiredMember startTime = "start_time";
        RequiredMember endTime = "end_time";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == worlds.Name)
            {
                worlds = member;
            }
            else if (member.Name == allWorlds.Name)
            {
                allWorlds = member;
            }
            else if (member.Name == startTime.Name)
            {
                startTime = member;
            }
            else if (member.Name == endTime.Name)
            {
                endTime = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchOverview
        {
            Id = id.Map(value => value.GetStringRequired()),
            Worlds = worlds.Map(value => value.GetWorlds(missingMemberBehavior)),
            AllWorlds = allWorlds.Map(value => value.GetAllWorlds(missingMemberBehavior)),
            StartTime = startTime.Map(value => value.GetDateTimeOffset()),
            EndTime = endTime.Map(value => value.GetDateTimeOffset())
        };
    }
}
