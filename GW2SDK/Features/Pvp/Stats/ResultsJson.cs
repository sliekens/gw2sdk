using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

internal static class ResultsJson
{
    public static Results GetResults(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Results
        {
            Wins = wins.Map(value => value.GetInt32()),
            Losses = losses.Map(value => value.GetInt32()),
            Desertions = desertions.Map(value => value.GetInt32()),
            Byes = byes.Map(value => value.GetInt32()),
            Forfeits = forfeits.Map(value => value.GetInt32())
        };
    }
}
