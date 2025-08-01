﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class LaddersJson
{
    public static Ladders GetLadders(this in JsonElement json)
    {
        OptionalMember none = "none";
        OptionalMember unranked = "unranked";
        OptionalMember ranked = "ranked";
        OptionalMember ranked2v2 = "2v2ranked";
        OptionalMember ranked3v3 = "3v3ranked";
        OptionalMember soloArenaRated = "soloarenarated";
        OptionalMember teamArenaRated = "teamarenarated";

        foreach (var member in json.EnumerateObject())
        {
            if (none.Match(member))
            {
                none = member;
            }
            else if (unranked.Match(member))
            {
                unranked = member;
            }
            else if (ranked.Match(member))
            {
                ranked = member;
            }
            else if (ranked2v2.Match(member))
            {
                ranked2v2 = member;
            }
            else if (ranked3v3.Match(member))
            {
                ranked3v3 = member;
            }
            else if (soloArenaRated.Match(member))
            {
                soloArenaRated = member;
            }
            else if (teamArenaRated.Match(member))
            {
                teamArenaRated = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Ladders
        {
            None = none.Map(static (in JsonElement value) => value.GetResults()),
            Unranked = unranked.Map(static (in JsonElement value) => value.GetResults()),
            Ranked = ranked.Map(static (in JsonElement value) => value.GetResults()),
            Ranked2v2 = ranked2v2.Map(static (in JsonElement value) => value.GetResults()),
            Ranked3v3 = ranked3v3.Map(static (in JsonElement value) => value.GetResults()),
            SoloArenaRated = soloArenaRated.Map(static (in JsonElement value) => value.GetResults()),
            TeamArenaRated = teamArenaRated.Map(static (in JsonElement value) => value.GetResults())
        };
    }
}
