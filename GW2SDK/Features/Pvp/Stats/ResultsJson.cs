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
        RequiredMember<int> wins = new("wins");
        RequiredMember<int> losses = new("losses");
        RequiredMember<int> desertions = new("desertions");
        RequiredMember<int> byes = new("byes");
        RequiredMember<int> forfeits = new("forfeits");

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
            Wins = wins.GetValue(),
            Losses = losses.GetValue(),
            Desertions = desertions.GetValue(),
            Byes = byes.GetValue(),
            Forfeits = forfeits.GetValue()
        };
    }
}
