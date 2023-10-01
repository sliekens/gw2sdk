using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

[PublicAPI]
public static class StandingJson
{
    public static Standing GetStanding(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> seasonId = new("season_id");
        RequiredMember<CurrentStanding> current = new("current");
        RequiredMember<BestStanding> best = new("best");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(seasonId.Name))
            {
                seasonId.Value = member.Value;
            }
            else if (member.NameEquals(current.Name))
            {
                current.Value = member.Value;
            }
            else if (member.NameEquals(best.Name))
            {
                best.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Standing
        {
            SeasonId = seasonId.GetValue(),
            Current = current.Select(value => value.GetCurrentStanding(missingMemberBehavior)),
            Best = best.Select(value => value.GetBestStanding(missingMemberBehavior))
        };
    }
}
