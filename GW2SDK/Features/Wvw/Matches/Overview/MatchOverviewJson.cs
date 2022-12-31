using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches.Overview;

[PublicAPI]
public static class MatchOverviewJson
{
    public static MatchOverview GetMatchOverview(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<Worlds> worlds = new("worlds");
        RequiredMember<AllWorlds> allWorlds = new("all_worlds");
        RequiredMember<DateTimeOffset> startTime = new("start_time");
        RequiredMember<DateTimeOffset> endTime = new("end_time");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(worlds.Name))
            {
                worlds.Value = member.Value;
            }
            else if (member.NameEquals(allWorlds.Name))
            {
                allWorlds.Value = member.Value;
            }
            else if (member.NameEquals(startTime.Name))
            {
                startTime.Value = member.Value;
            }
            else if (member.NameEquals(endTime.Name))
            {
                endTime.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MatchOverview
        {
            Id = id.GetValue(),
            Worlds = worlds.Select(value => value.GetWorlds(missingMemberBehavior)),
            AllWorlds = allWorlds.Select(value => value.GetAllWorlds(missingMemberBehavior)),
            StartTime = startTime.GetValue(),
            EndTime = endTime.GetValue()
        };
    }
}
