using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class StandingJson
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
            if (seasonId.Match(member))
            {
                seasonId = member;
            }
            else if (current.Match(member))
            {
                current = member;
            }
            else if (best.Match(member))
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
            SeasonId = seasonId.Map(value => value.GetStringRequired()),
            Current = current.Map(value => value.GetCurrentStanding(missingMemberBehavior)),
            Best = best.Map(value => value.GetBestStanding(missingMemberBehavior))
        };
    }
}
