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
        RequiredMember seasonId = "season_id";
        RequiredMember current = "current";
        RequiredMember best = "best";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(seasonId.Name))
            {
                seasonId = member;
            }
            else if (member.NameEquals(current.Name))
            {
                current = member;
            }
            else if (member.NameEquals(best.Name))
            {
                best = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Standing
        {
            SeasonId = seasonId.Select(value => value.GetStringRequired()),
            Current = current.Select(value => value.GetCurrentStanding(missingMemberBehavior)),
            Best = best.Select(value => value.GetBestStanding(missingMemberBehavior))
        };
    }
}
