using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

[PublicAPI]
public static class ResultsJson
{
    public static Results GetResults(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember wins = new("wins");
        RequiredMember losses = new("losses");
        RequiredMember desertions = new("desertions");
        RequiredMember byes = new("byes");
        RequiredMember forfeits = new("forfeits");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(wins.Name))
            {
                wins.Value = member.Value;
            }
            else if (member.NameEquals(losses.Name))
            {
                losses.Value = member.Value;
            }
            else if (member.NameEquals(desertions.Name))
            {
                desertions.Value = member.Value;
            }
            else if (member.NameEquals(byes.Name))
            {
                byes.Value = member.Value;
            }
            else if (member.NameEquals(forfeits.Name))
            {
                forfeits.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Results
        {
            Wins = wins.Select(value => value.GetInt32()),
            Losses = losses.Select(value => value.GetInt32()),
            Desertions = desertions.Select(value => value.GetInt32()),
            Byes = byes.Select(value => value.GetInt32()),
            Forfeits = forfeits.Select(value => value.GetInt32())
        };
    }
}
