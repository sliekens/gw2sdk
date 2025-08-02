using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Standings;

internal static class StandingJson
{
    public static Standing GetStanding(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Standing
        {
            SeasonId = seasonId.Map(static (in JsonElement value) => value.GetStringRequired()),
            Current = current.Map(static (in JsonElement value) => value.GetCurrentStanding()),
            Best = best.Map(static (in JsonElement value) => value.GetBestStanding())
        };
    }
}
