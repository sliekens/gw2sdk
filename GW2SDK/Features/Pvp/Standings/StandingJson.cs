using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class StandingJson
{
    public static Standing GetStanding(
        this JsonElement json
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Standing
        {
            SeasonId = seasonId.Map(static value => value.GetStringRequired()),
            Current = current.Map(static value => value.GetCurrentStanding()),
            Best = best.Map(static value => value.GetBestStanding())
        };
    }
}
