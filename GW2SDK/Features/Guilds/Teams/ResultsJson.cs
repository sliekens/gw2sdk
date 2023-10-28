using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

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
            if (member.Name == wins.Name)
            {
                wins = member;
            }
            else if (member.Name == losses.Name)
            {
                losses = member;
            }
            else if (member.Name == desertions.Name)
            {
                desertions = member;
            }
            else if (member.Name == byes.Name)
            {
                byes = member;
            }
            else if (member.Name == forfeits.Name)
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
