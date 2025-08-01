﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class ResultsJson
{
    public static Results GetResults(this in JsonElement json)
    {
        RequiredMember wins = "wins";
        RequiredMember losses = "losses";
        RequiredMember desertions = "desertions";
        RequiredMember byes = "byes";
        RequiredMember forfeits = "forfeits";

        foreach (var member in json.EnumerateObject())
        {
            if (wins.Match(member))
            {
                wins = member;
            }
            else if (losses.Match(member))
            {
                losses = member;
            }
            else if (desertions.Match(member))
            {
                desertions = member;
            }
            else if (byes.Match(member))
            {
                byes = member;
            }
            else if (forfeits.Match(member))
            {
                forfeits = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Results
        {
            Wins = wins.Map(static (in JsonElement value) => value.GetInt32()),
            Losses = losses.Map(static (in JsonElement value) => value.GetInt32()),
            Desertions = desertions.Map(static (in JsonElement value) => value.GetInt32()),
            Byes = byes.Map(static (in JsonElement value) => value.GetInt32()),
            Forfeits = forfeits.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
